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
            var yesterday = DateTime.Now.AddDays(-1);
            var theDayBeforeYesterDay = DateTime.Now.AddDays(-2);
            var list = context.News.Where(n => n.IsPublished ?? false
                    && (n.OriginalDate.Value.Date == DateTime.Now.Date
                        || n.OriginalDate.Value.Date == yesterday.Date)
                        ).ToList();
            return View(list);
        }
    }
}