using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ICategoryService _categoryService;

        public CategoryController(AppDbContext context,
                                  ICategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }



        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.GetAll();

            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            try
            {
                if (!ModelState.IsValid) return View();

                Category existedCatgeory = await _context.Categories.FirstOrDefaultAsync(c => c.Name.Trim().ToLower() == category.Name.Trim().ToLower());

                if (existedCatgeory is not null)
                {
                    ModelState.AddModelError("Name", "Category with this name is already exists");
                    return View();
                }

                //throw new Exception("Model statetimiz bugun bizi yolda qoydu");

                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetById(id);
            if (category is null) return NotFound();

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetById(id);
            if (category is null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (!ModelState.IsValid) return View();

            Category dbCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == category.Id);

            if (category.Name.Trim() == dbCategory.Name.Trim()) return RedirectToAction(nameof(Index));

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            Category category = await _categoryService.GetById(id);
            if (category is null) return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}