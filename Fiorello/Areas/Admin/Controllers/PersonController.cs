using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IExpertService _expertService;

        public PersonController(AppDbContext context, IExpertService expertService)
        {
            _context = context;
            _expertService = expertService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Person> persons = await _expertService.GetAllPersons();

            return View(persons);
        }
    }
}