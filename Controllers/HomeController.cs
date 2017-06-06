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
            var list = await context.News.Where(n => ((n.IsPublished ?? false)
                                                    && NearXDays(n.OriginalDate.Value, 10))
                                                ).ToListAsync();
            return View(list);
        }

        private bool NearXDays( DateTime date, int i )
        {
            // if i == 1, only today, yicileitui
            var now = DateTime.Now;
            DateTime near;
            for (int k = 0; k < i; ++k)
            {
                near = now.AddDays(-k);
                if (date.Date.Equals(near.Date))
                {
                    return true;
                }
            }

            return false;
        }
    }
}