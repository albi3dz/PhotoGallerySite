using PhotoGallerySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallerySite.Controllers
{
    public class ImageController : Controller
    {
        ApplicationDbContext _db;
        // GET: MyAlbums
        public ImageController()
        {
            _db = new ApplicationDbContext();
        }
        public ActionResult Show(int Id)
        {
            var image = _db.Pictures.Where(p => p.PictureId == Id).Single().Content;
            return File(image, "image/jpg");
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