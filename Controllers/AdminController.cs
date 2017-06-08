using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using UniversityNews_aNewTry.Models;

namespace UniversityNews_aNewTry.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly NewsContext context;

        public AdminController( NewsContext nc )
        {
            context = nc;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await context.News
                .SingleOrDefaultAsync(m => m.Title == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        public async Task<IActionResult> EditBUAA()
        {
            ViewData["UniName"] = "�������պ����ѧ";
            var list = await context.News.Where(n => n.UniversityName == "BUAA").ToListAsync();
            list.Sort((a, b) =>
            {
                var aOrigDate = a.OriginalDate ?? DateTime.MinValue;
                var bOrigDate = b.OriginalDate ?? DateTime.MinValue;
                if (aOrigDate < bOrigDate)
                {
                    return 1;
                }
                else if (aOrigDate > bOrigDate)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            return View("Edit", list);
        }

        public async Task<IActionResult> EditPKU()
        {
            ViewData["UniName"] = "������ѧ";
            var list = await context.News.Where(n => n.UniversityName == "PKU").ToListAsync();
            list.Sort((a, b) =>
            {
                var aOrigDate = a.OriginalDate ?? DateTime.MinValue;
                var bOrigDate = b.OriginalDate ?? DateTime.MinValue;
                if (aOrigDate < bOrigDate)
                {
                    return 1;
                }
                else if (aOrigDate > bOrigDate)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            return View("Edit", list);
        }

        public async Task<IActionResult> EditTHU()
        {
            ViewData["UniName"] = "�廪��ѧ";
            var list = await context.News.Where(n => n.UniversityName == "THU").ToListAsync();
            list.Sort((a, b) =>
            {
                var aOrigDate = a.OriginalDate ?? DateTime.MinValue;
                var bOrigDate = b.OriginalDate ?? DateTime.MinValue;
                if (aOrigDate < bOrigDate)
                {
                    return 1;
                }
                else if (aOrigDate > bOrigDate)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            return View("Edit", list);
        }

        public async Task<IActionResult> EditRUC()
        {
            ViewData["UniName"] = "�й������ѧ";
            var list = await context.News.Where(n => n.UniversityName == "RUC").ToListAsync();
            list.Sort((a, b) =>
            {
                var aOrigDate = a.OriginalDate ?? DateTime.MinValue;
                var bOrigDate = b.OriginalDate ?? DateTime.MinValue;
                if (aOrigDate < bOrigDate)
                {
                    return 1;
                }
                else if (aOrigDate > bOrigDate)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            return View("Edit", list);
        }

        public async Task<IActionResult> Publish(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await context.News.SingleAsync(m => m.Title == id);
            if (news == null)
            {
                return NotFound();
            }

            if (news.IsPublished ?? false)
            {
                news.IsPublished = false;
                news.PublishDate = null;
            }
            else
            {
                news.IsPublished = true;
                news.PublishDate = DateTime.Now;
            }

            try
            {
                context.Update(news);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException duce)
            {
                if (!NewsExists(news.Title))
                {
                    return NotFound();
                }
                else
                    throw;
            }

            if (news.UniversityName == "BUAA")
            {
                return RedirectToAction("EditBuaa");
            }
            else if (news.UniversityName == "PKU")
            {
                return RedirectToAction("EditPKU");
            }
            else if (news.UniversityName == "THU")
            {
                return RedirectToAction("EditTHU");
            }
            else if (news.UniversityName == "RUC")
            {
                return RedirectToAction("EditRUC");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Publish( News news )
        {
            Console.WriteLine("hhhhh");
            return RedirectToAction("EditBUAA");
        }



        private bool NewsExists(string id)
        {
            return context.News.Any(e => e.Title == id);
        }
    }
}