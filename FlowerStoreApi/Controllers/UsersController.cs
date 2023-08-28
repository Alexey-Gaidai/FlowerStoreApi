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
    public class UsersController : ControllerBase
    {
        private readonly FlowerStoreDBContext _context;

        public UsersController(FlowerStoreDBContext context)
        {
            _context = context;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Ищем пользователя по логину (в данном случае, email)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == username);

            if (user == null || user.Password != password)
            {
                // Если пользователь не найден или пароль не совпадает, возвращаем код 401 (Unauthorized)
                return Unauthorized(new UserLoginInfo());
            }

            var userinfo = new UserLoginInfo
            {
                id = user.ID,
                name = user.FirstName
            };


            // Пользователь найден, возвращаем код 200 (Ok)
            return Ok(userinfo);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO registerModel)
        {
            // Проверяем, существует ли пользователь с таким же email
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerModel.Email);
            if (existingUser != null)
            {
                Message msg = new Message("Пользователь с таким email уже существует.");
                return Ok(msg);
            }

            // Создаем нового пользователя
            var newUser = new User
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                Phone = registerModel.Phone,
                Password = registerModel.Password
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            Message message = new Message("Пользователь успешно зарегистрирован.");
            return Ok(message);
        }

        // GET: api/User/{userId}/Orders
        [HttpGet("{userId}/Orders")]
        public IActionResult GetUserOrders(int userId)
        {
            try
            {
                var user = _context.Users.Include(u => u.Orders)
                                         .ThenInclude(o => o.OrderedBouquets)
                                         .ThenInclude(ob => ob.Bouquet)
                                         .SingleOrDefault(u => u.ID == userId);

                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                var userOrders = user.Orders.Select(order => new
                {
                    OrderId = order.ID,
                    OrderDate = order.OrderDate.ToShortDateString(),
                    Address = $"{order.City}, {order.Street}, {order.House}, {order.Apartment}",
                    Bouquets = order.OrderedBouquets.Select(ob => new
                    {
                        ID = ob.Bouquet.ID,
                        Name = ob.Bouquet.Name,
                        ImageUrl = ob.Bouquet.ImageUrl,
                        Quantity = ob.Quantity,
                        Price = ob.Bouquet.Price
                    }),
                });

                return Ok(userOrders);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(u => u.ID == userId);

                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }

                var userInfo = new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email
                };

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                // Handle exception and return appropriate response
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
