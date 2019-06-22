using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cloud_homework4.Models;

namespace Cloud_homework4.Controllers
{
    [ApiController, Route("api"), Produces("application/json")]
    public class RestaurantsAPIController : ControllerBase
    {
        private readonly Cloud_homework4Context _context;

        public RestaurantsAPIController(Cloud_homework4Context context)
        {
            _context = context;
        }

        // GET: api/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurant()
        {
            return new JsonResult(await _context.Restaurant.ToListAsync());
        }

        // GET: api/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);

            if (restaurant == null)
            {
                return NotFound();
            }

            return new JsonResult(restaurant);
        }

        // GET: api/genres/
        [HttpGet("genres")]
        public async Task<ActionResult<Restaurant>> GetRestaurant_()
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from restaurant in _context.Restaurant orderby restaurant.Genre select restaurant.Genre;

            return new JsonResult(await genreQuery.Distinct().ToListAsync());
        }

        // GET: api/genre/{str_genre}
        [HttpGet("genre/{str_genre}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant_(string str_genre)
        {
            // Important: 查詢｢只｣會在此時定義，它尚未對資料庫執行。
            var data = from restaurant in _context.Restaurant select restaurant;

            if (!String.IsNullOrEmpty(str_genre))
            {
                data = data.Where(s => s.Genre.Equals(str_genre));
            }

            return new JsonResult(await data.Distinct().ToListAsync());
        }

        // GET: api/search/{str_search}
        [HttpGet("search/{str_search}")]
        public async Task<ActionResult<Restaurant>> GetRestaurant_1(string str_search)
        {
            // Important: 查詢｢只｣會在此時定義，它尚未對資料庫執行。
            var data = from restaurant in _context.Restaurant select restaurant;

            if (!String.IsNullOrEmpty(str_search))
            {
                data = data.Where(s => s.Name.Contains(str_search));
            }

            return new JsonResult(await data.Distinct().ToListAsync());
        }

        // POST: api/
        [HttpPost]
        public async Task<ActionResult<Restaurant>> PostRestaurant(Restaurant restaurant)
        {
            restaurant.ReleaseDate = DateTime.Now.Date;
            _context.Restaurant.Add(restaurant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, restaurant);
        }

        // DELETE: api/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Restaurant>> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();

            return restaurant;
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.Id == id);
        }
    }
}
