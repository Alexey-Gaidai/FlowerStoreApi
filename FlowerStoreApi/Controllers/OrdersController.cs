using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlowerStoreApi.Data;
using FlowerStoreApi.Data.Models;
using FlowerStoreApi.Data.Models.DTO;

namespace FlowerStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly FlowerStoreDBContext _context;

        public OrdersController(FlowerStoreDBContext context)
        {
            _context = context;
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO createOrderDTO)
        {
            Console.WriteLine(createOrderDTO.UserID.ToString());
            Console.WriteLine(createOrderDTO.City.ToString());
            Console.WriteLine(createOrderDTO.OrderedBouquets[0].BouquetID);
            try
            {
                var newOrder = new Order
                {
                    UserID = createOrderDTO.UserID,
                    OrderDate = DateTime.Now,
                    OrderStatus = "Pending",
                    City = createOrderDTO.City,
                    Street = createOrderDTO.Street,
                    House = createOrderDTO.House,
                    Apartment = createOrderDTO.Apartment
                };

                _context.Orders.Add(newOrder);
                

                foreach (var bouquet in createOrderDTO.OrderedBouquets)
                {
                    var newOrderedBouquets = new OrderedBouquets
                    {
                        OrderID = newOrder.ID,
                        BouquetID = bouquet.BouquetID,
                        Quantity = bouquet.Quantity
                    };
                }

                await _context.SaveChangesAsync();

                return Ok(new Message("Заказ успешно создан"));
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
