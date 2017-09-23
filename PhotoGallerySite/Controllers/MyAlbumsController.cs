using PhotoGallerySite.ActionResults;
using PhotoGallerySite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace PhotoGallerySite.Controllers
{
    [Authorize]
    public class MyAlbumsController : Controller
    {
        ApplicationDbContext _db;
        // GET: MyAlbums
        public MyAlbumsController()
        {
            _db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            string UserName = User.Identity.Name;
            var MyAlbums = _db.Users.Where(u => u.UserName == UserName).First().Albums;
            return View(MyAlbums);
        }

        [HttpGet]
        public ActionResult AddAlbum()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAlbum(AlbumViewModel Album)
        {
            Album data = new Album()
            {
                AlbumTitle = Album.AlbumTitle,
                Description = Album.Description,
                UserId = _db.Users.Where(u => u.UserName == User.Identity.Name).First().Id,
                Public = Album.Public,
                Pictures = new List<Picture>()
            };
            foreach (var pic in Album.Pictures)
            {
                byte[] buffer = pic.ToByte();
                if (buffer == null) continue;
                data.Pictures.Add(new Picture
                {
                    PictureTitle = pic.PictureTitle,
                    Content = buffer,
                });
            }
            _db.Albums.Add(data);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int Id)
        {
            var Album = _db.Albums.Where(a => a.AlbumId == Id).Single();
            if (User.Identity.Name != Album.User.UserName)
                return new Http403Result();
            int i = Album.Pictures.Count - 1;
            while (i >= 0)
            {
                _db.Pictures.Remove(Album.Pictures.ElementAt(i));
                i--;
            }
            _db.Comments.RemoveRange(Album.Comments);
            _db.Albums.Remove(Album);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Album album = _db.Albums.Where(a => a.AlbumId == Id).Single();
            if (User.Identity.Name != album.User.UserName)
                return new Http403Result();
            return View(album);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(AlbumViewModel albumview)
        {
            Album album = albumview.ToAlbum();
            album.UserId = _db.Albums.Where(a => a.AlbumId == album.AlbumId).Single().UserId;
            var OldPics = _db.Pictures.Where(a => a.AlbumId == album.AlbumId).ToList();
            List<Picture> ToDel = new List<Picture>(OldPics);
            for (int i = 0; i < album.Pictures.Count; i++)
            {
                var e = album.Pictures.ElementAt(i);
                e.AlbumId = album.AlbumId;
                if (e.Content == null)
                {
                    e.Content = OldPics.Where(p => p.PictureId == e.PictureId).Single().Content;
                }
                ToDel.RemoveAll(p => p.PictureId == e.PictureId);
                _db.Set<Picture>().AddOrUpdate(e);
            }
            int j = ToDel.Count() - 1;
            while (j >= 0)
            {
                _db.Pictures.Remove(ToDel[j]);
                j--;
            }
            _db.Set<Album>().AddOrUpdate(album);
            _db.SaveChanges();
            return RedirectToAction("Edit");
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