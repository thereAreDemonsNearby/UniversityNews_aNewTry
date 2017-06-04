using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversityNews_aNewTry.Models;

namespace UniversityNews_aNewTry.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewsContext context;

        public HomeController( NewsContext nc )
        {
            context = nc;
        }

        public async Task<IActionResult> Index()
        {
            var list = await context.News.Where(n => n.IsPublished ?? false).ToListAsync();
            return View(list);
        }
    }
}