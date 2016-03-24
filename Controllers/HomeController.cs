using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HaveFun.Models;

namespace HaveFun.Controllers
{
    public class HomeController : Controller
    {
        private readonly BlogDataContext _db;

        public HomeController(BlogDataContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page = 0)
        {
            var pageSize = 5;
            var skip = page * pageSize;

            var posts =
                _db.Posts
                    .OrderByDescending(x => x.Time)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToArray();

            var totalPosts = _db.Posts.Count();
            var totalPages = totalPosts / pageSize;
            var previousPage = page - 1;
            var nextPage = page + 1;

            ViewBag.PreviousPage = previousPage;
            ViewBag.HasPreviousPage = previousPage >= 0;
            ViewBag.NextPage = nextPage;
            ViewBag.HasNextPage = nextPage < totalPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView(posts);

            return View(posts);
        }

       
        

        public IActionResult About()
        {
            ViewData["Message"] = "Life";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "";

            return View();
        }

        public IActionResult Boast()
        {

            return RedirectToRoute(new
            {
                controller = "Posts",
                action = "Create",

            });
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
