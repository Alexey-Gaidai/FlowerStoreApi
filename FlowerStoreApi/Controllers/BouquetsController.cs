using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerStoreApi.Data;
using FlowerStoreApi.Data.Models;

namespace FlowerStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BouquetsController : ControllerBase
    {
        private readonly FlowerStoreDBContext _context;

        public BouquetsController(FlowerStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Bouquets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bouquet>>> GetBouquets()
        {
          if (_context.Bouquets == null)
          {
              return NotFound();
          }
            return await _context.Bouquets.ToListAsync();
        }

        // GET: api/Bouquets/GetByFlower/{flowerId}
        [HttpGet("GetByFlower/{flowerId}")]
        public IActionResult GetByFlower(int flowerId)
        {
            var bouquets = _context.BouquetCopmosition
                .Where(bc => bc.FlowerID == flowerId)
                .Select(bc => bc.Bouquet)
                .ToList();

            if (bouquets == null || bouquets.Count == 0)
            {
                return NotFound("No bouquets found with the given flower.");
            }

            return Ok(bouquets);
        }

        // GET: api/Bouquet/{bouquetId}
        [HttpGet("{bouquetId}")]
        public IActionResult GetBouquetDetails(int bouquetId)
        {
            var bouquet = _context.Bouquets
                .Include(b => b.BouquetCopmosition)
                    .ThenInclude(bc => bc.Flower)
                .FirstOrDefault(b => b.ID == bouquetId);

            if (bouquet == null)
            {
                return NotFound("Bouquet not found.");
            }

            // Создаем анонимный объект с данными о букете и его составе
            var bouquetWithComposition = new
            {
                bouquet.ID,
                bouquet.Name,
                bouquet.ImageUrl,
                bouquet.Description,
                bouquet.Price,
                BouquetComposition = bouquet.BouquetCopmosition.Select(bc => new
                {
                    bc.ID,
                    bc.FlowerID,
                    bc.Quantity,
                    FlowerName = bc.Flower.Name // Включаем имя цветка
                })
            };

            return Ok(bouquetWithComposition);
        }


        [HttpGet("GetTopSelling")]
        public IActionResult GetTopSelling()
        {
            var topBouquets = _context.OrderedBouquets
                .GroupBy(ob => ob.Bouquet)
                .OrderByDescending(group => group.Sum(ob => ob.Quantity))
                .Take(10)
                .Select(group => group.Key)
                .ToList();

            if (topBouquets == null || topBouquets.Count == 0)
            {
                return NotFound("No bouquets found.");
            }

            return Ok(topBouquets);
        }

        private bool BouquetExists(int id)
        {
            return (_context.Bouquets?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
