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

        public IActionResult EditBUAA()
        {
            ViewData["UniName"] = "BUAA";
            var list = context.News.Where(n => n.UniversityName == "BUAA").ToList();
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
                    //var aPubDate = a.PublishDate ?? DateTime.MinValue;
                    //var bPubDate = b.PublishDate ?? DateTime.MinValue;
                    //if £¨aPubDate < bPubDate)
                    //{
                    //    return -1;
                    //}
                    //else 
                    //{
                    //    return 1;
                    //}
                    return 0;
                }
            });
            return View("Edit", list);
        }

        public IActionResult EditPKU()
        {
            ViewData["UniName"] = "PKU";
            var list = context.News.Where(n => n.UniversityName == "PKU").ToList();
            //list.Sort((a, b) =>
            //{
            //    var aOrigVal = a.OriginalDate != null ? aOrigVal : DateTime.MinValue;
            //});
            return View("Edit", list);
        }

        public IActionResult EditTHU()
        {
            ViewData["UniName"] = "THU";
            var list = context.News.Where(n => n.UniversityName == "THU").ToList();
            list.Sort((a, b) =>
            {
                if ((a.IsPublished ?? false) == false &&
                    (b.IsPublished ?? false) == true)
                {
                    return 1;
                }
                else if ((a.IsPublished ?? false) == true
                        && (b.IsPublished ?? false) == false)
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

        public IActionResult EditCCMU()
        {
            ViewData["UniName"] = "CCMU";
            var list = context.News.Where(n => n.UniversityName == "CCMU").ToList();
            list.Sort((a, b) =>
            {
                if ((a.IsPublished ?? false) == false &&
                    (b.IsPublished ?? false) == true)
                {
                    return 1;
                }
                else if ((a.IsPublished ?? false) == true
                        && (b.IsPublished ?? false) == false)
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
                context.SaveChanges();
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
            else if (news.UniversityName == "CCMU")
            {
                return RedirectToAction("EditCCMU");
            }
            else
            {
                return NotFound();
            }
        }



        private bool NewsExists(string id)
        {
            return context.News.Any(e => e.Title == id);
        }
    }
}