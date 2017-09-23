using PhotoGallerySite.ActionResults;
using PhotoGallerySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallerySite.Controllers
{
    public class AlbumsController : Controller
    {
        ApplicationDbContext _db;
        public AlbumsController()
        {
            _db = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult Index(int Id, int PicId = -1, bool backward = true)
        {
            var album = _db.Albums.Where(a => a.AlbumId == Id).Single();
            if (!album.Public && User.Identity.Name != album.User.UserName) return new Http403Result();
            var showdata = new AlbumShow()
            {
                AlbumId = album.AlbumId,
                AlbumTitle = album.AlbumTitle,
                Description = album.Description,
                Comments = _db.Comments.Where(c => c.AlbumId == album.AlbumId).ToList()
            };

            if (Request.IsAjaxRequest())
            {
                var tmp = album.Pictures.ToArray();
                int ind = Array.FindIndex(tmp, p => p.PictureId == PicId);
                if (backward)
                    if (ind - 2 < 0)
                        ind = 0;
                    else
                        ind -= 2;
                else
                    if (ind + 2 > tmp.Length - 1)
                    ind = tmp.Length - 3;
                showdata.Pictures = new PictureShow[] {
                   new PictureShow() { PictureId=tmp[ind].PictureId, PictureTitle=tmp[ind].PictureTitle},
                   new PictureShow() { PictureId=tmp[ind+1].PictureId, PictureTitle=tmp[ind+1].PictureTitle},
                    new PictureShow() { PictureId=tmp[ind+2].PictureId, PictureTitle=tmp[ind+2].PictureTitle},
                };

                return PartialView("_ImagePreview", showdata);
            }
            var First3 = album.Pictures.Take(3).ToArray();
            showdata.Pictures = new PictureShow[] {
                   new PictureShow() { PictureId=First3[0].PictureId, PictureTitle=First3[0].PictureTitle},
                   new PictureShow() { PictureId=First3[1].PictureId, PictureTitle=First3[1].PictureTitle},
                    new PictureShow() { PictureId=First3[2].PictureId, PictureTitle=First3[2].PictureTitle},
                };


            return View(showdata);
        }

        [ValidateAntiForgeryToken]
        [Authorize]
        [HttpPost]
        public ActionResult AddComment(int Id, string Content)
        {
            var album = _db.Albums.Where(a => a.AlbumId == Id).Single();
            if (!album.Public && User.Identity.Name != album.User.UserName) return new Http403Result();
            Comment comment = new Comment()
            {
                Content = Content,
                AlbumId = Id,
                UserName = User.Identity.Name,
                DateCreated = DateTime.Now
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            var showdata = new AlbumShow()
            {
                AlbumId = Id,
                Comments = _db.Comments.Where(c => c.AlbumId == Id).ToList()
            };
            return PartialView("_Comments", showdata);
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