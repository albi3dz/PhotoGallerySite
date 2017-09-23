using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGallerySite.Models
{
    public class AlbumShow
    {
        public int AlbumId { get; set; }
        public string AlbumTitle { get; set; }
        public string Description { get; set; }
        public PictureShow[] Pictures { get; set; } = new PictureShow[3];
        public List<Comment> Comments { get; set; }

    }
}