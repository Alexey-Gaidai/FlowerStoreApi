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
    public class FlowersController : ControllerBase
    {
        private readonly FlowerStoreDBContext _context;

        public FlowersController(FlowerStoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Flowers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flower>>> GetFlowers()
        {
          if (_context.Flowers == null)
          {
              return NotFound();
          }
            return await _context.Flowers.ToListAsync();
        }

        private bool FlowerExists(int id)
        {
            return (_context.Flowers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
