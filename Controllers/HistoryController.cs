using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityNews_aNewTry.Models;
using Microsoft.EntityFrameworkCore;

namespace UniversityNews_aNewTry.Controllers
{
    public class HistoryController : Controller
    {
        private readonly NewsContext context;

        public HistoryController( NewsContext nc)
        {
            context = nc;
        }

        public async Task<IActionResult> Index( DateTime? dt )
        {
            DateTime date = dt ?? DateTime.Now;

            var list = await context.News.Where(n => (n.IsPublished ?? false)
                                    && n.OriginalDate.Value.Date == date.Date)
                                    .OrderByDescending(n => n.OriginalDate).ToListAsync();
            return View(list);
        }
    }
}