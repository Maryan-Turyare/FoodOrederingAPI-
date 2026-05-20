using Microsoft.AspNetCore.Mvc;
using FoodOrederingAPI.Data;
using FoodOrederingAPI.Models;

namespace FoodOrederingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_context.Orders.ToList());
        }

        [HttpPost]
        public IActionResult AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            order.CustomerName = updatedOrder.CustomerName;
            order.FoodId = updatedOrder.FoodId;
            order.Quantity = updatedOrder.Quantity;

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return Ok("Order deleted successfully");
        }
    }
}