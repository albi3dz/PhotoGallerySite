using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PhotoGallerySite.Models
{
    public class Picture
    {
        public int PictureId { get; set; }
        public string PictureTitle { get; set; }
        public byte[] Content { get; set; }
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

    }
}