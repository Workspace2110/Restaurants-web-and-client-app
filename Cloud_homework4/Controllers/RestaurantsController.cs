using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cloud_homework4.Models;

namespace Cloud_homework4.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly Cloud_homework4Context _context;

        public RestaurantsController(Cloud_homework4Context context)
        {
            _context = context;
        }

        // GET: Restaurants
        [HttpGet, ActionName("Index")]
        public async Task<IActionResult> Index(string str_genres, string str_search)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from restaurant in _context.Restaurant orderby restaurant.Genre select restaurant.Genre;

            // Important: 查詢｢只｣會在此時定義，它尚未對資料庫執行。
            var data = from restaurant in _context.Restaurant select restaurant;

            if (!String.IsNullOrEmpty(str_search))
            {
                data = data.Where(s => s.Name.Contains(str_search));
            }

            if (!String.IsNullOrEmpty(str_genres))
            {
                data = data.Where(s => s.Genre.Equals(str_genres));
            }

            var genreVM = new RestaurantGenreModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Restaurants = await data.ToListAsync()
            };

            return View(genreVM);
        }

        // Test if posting is right working
        [HttpPost, ActionName("Index")]
        public string Index(string str_search, bool isUsed = false)
        {
            return "From [HttpPost]Index: filter on " + str_search;
        }

        // GET: Restaurants/Details/5
        [HttpGet, ActionName("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // GET: Restaurants/Create
        [HttpGet, ActionName("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Genre,MinPrice,MaxPrice,ReleaseDate")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.ReleaseDate = DateTime.Now.Date;
                _context.Add(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [HttpGet, ActionName("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Genre,MinPrice,MaxPrice,ReleaseDate")] Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    restaurant.ReleaseDate = DateTime.Now.Date;
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantExists(restaurant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurant = await _context.Restaurant.FindAsync(id);
            _context.Restaurant.Remove(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.Id == id);
        }
    }
}
