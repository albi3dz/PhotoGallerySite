using PhotoGallerySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallerySite.Controllers
{
    public class SearchController : Controller
    {
        ApplicationDbContext _db;
        // GET: MyAlbums
        public SearchController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index(string condition = null, string pattern = null)
        {
            if (Request.IsAjaxRequest())
            {
                if (pattern == "" || pattern == null)
                {
                    var d = _db.Albums.Where(a => a.Public == true).ToList();

                    return PartialView("_SearchResults", _db.Albums.Where(a => a.Public == true).ToList());
                }
                List<Album> model = null;
                if (condition == "user")
                {
                    model = _db.Albums
                    .Where(a => a.User.UserName.ToLower().Contains(pattern.ToLower()) && a.Public == true)
                .ToList();
                }
                else
                {
                    model = _db.Albums
                    .Where(a => a.AlbumTitle.ToLower().Contains(pattern.ToLower()) && a.Public == true)
                .ToList();
                }
                if (model.Count == 0)
                    return PartialView("_NoResults");
                return PartialView("_SearchResults", model);
            }
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}