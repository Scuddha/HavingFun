using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using HaveFun.Models;


namespace HaveFun.Controllers
{
    public class PostsController : Controller
    {
        readonly BlogDataContext _dataContext;

        public PostsController(BlogDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            if (!ModelState.IsValid)
                return View(post);

            post.Time = DateTime.Now;
            post.Author = User.Identity.Name;

            
            _dataContext.Posts.Add(post);
            await _dataContext.SaveChangesAsync();

            return RedirectToAction("Post", new { post.Time.Year, post.Time.Month, post.Key });
        }

        public IActionResult Post(long id)

        {
           

            var post = _dataContext.Posts.SingleOrDefault(x => x.Id == id);
            
            return View(post);

        }

        [Route("post/{year}/{month:int}/{key}")]
        public IActionResult Post(int year, int month, string key)

        {

            var post = _dataContext.Posts.SingleOrDefault(
                x => x.Time.Year == year && x.Time.Month == month
                    && x.Key == key.ToLower());
                

            return View(post);

        }
    }
}
