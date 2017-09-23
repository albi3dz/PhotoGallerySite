using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace PhotoGallerySite.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public int AlbumId { get; set; }
    }
}