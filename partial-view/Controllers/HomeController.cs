using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using partial_view.DataAccessLayer;
using partial_view.Models;

namespace partial_view.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _itemsCount;

        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _itemsCount = _dbContext.Products.Count();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.ItemsCount = _itemsCount;
            var categories = await _dbContext.Categories.ToListAsync();
            var products = await _dbContext.Products.Take(5).ToListAsync();

            var model = new HomeViewModel()
            {
                Categories = categories,
                Products = products,
            };

            return View(model);
        }

        public async Task<IActionResult> Partial(int skip)
        {
            var products = await _dbContext.Products.Skip(skip).Take(5).ToListAsync();

            return PartialView("_ProductsPartial", products);
        }

      
    }
}
