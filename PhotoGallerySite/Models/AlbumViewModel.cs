using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallerySite.Models
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string Description { get; set; }
        public bool Public { get; set; }
        public ICollection<PictureViewModel> Pictures { get; set; }
        public Album ToAlbum()
        {
            var album = new Album();
            album.AlbumId = AlbumId;
            album.AlbumTitle = AlbumTitle;
            album.Description = Description;
            album.Public = Public;
            album.Pictures = new List<Picture>();
            foreach (var pic in Pictures)
            {
                byte[] buffer = pic.ToByte();
                album.Pictures.Add(new Picture() { PictureTitle = pic.PictureTitle, PictureId = pic.PictureId, Content = buffer });
            }
            return album;
        }
    }
}