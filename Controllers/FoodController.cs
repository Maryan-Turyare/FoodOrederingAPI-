using Microsoft.AspNetCore.Mvc;
using FoodOrederingAPI.Data;
using FoodOrederingAPI.Models;

namespace FoodOrederingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Food
        [HttpGet]
        public IActionResult GetFoods()
        {
            return Ok(_context.Foods.ToList());
        }

        // POST: api/Food
        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            _context.Foods.Add(food);
            _context.SaveChanges();
            return Ok(food);
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food updatedFood)
        {
            var food = _context.Foods.Find(id);

            if (food == null)
                return NotFound();

            food.Name = updatedFood.Name;
            food.Price = updatedFood.Price;
            food.Description = updatedFood.Description;

            _context.SaveChanges();

            return Ok(food);
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteFood(int id)
        {
            var food = _context.Foods.Find(id);

            if (food == null)
                return NotFound();

            _context.Foods.Remove(food);
            _context.SaveChanges();

            return Ok("Food deleted successfully");
        }
    }
}