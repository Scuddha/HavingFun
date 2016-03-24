using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaveFun.Models;

namespace HaveFun.ViewComponents
{
    [ViewComponent]
    public class ArchivedPostsViewComponent : ViewComponent
    {
        private BlogDataContext _db;

        public ArchivedPostsViewComponent(HaveFun.Models.BlogDataContext db)
        {
            _db = db;
        }


        public IViewComponentResult Invoke()
        {
            var archivedPosts = _db.GetArchivedPosts().ToArray();
            return View(archivedPosts);
        }
    }
}
