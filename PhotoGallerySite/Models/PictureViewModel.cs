using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoGallerySite.Models
{
    public class PictureViewModel
    {
        public int PictureId { get; set; }
        public string PictureTitle { get; set; }
        public HttpPostedFileBase Content { get; set; }
        public byte[] ToByte()
        {
            byte[] buffer = null;
            string[] ext = { ".jpg", ".png", ".gif", ".bmp", ".jpeg" };
            if (Content != null && ext.Contains(Path.GetExtension(Content.FileName).ToLower()))
                using (MemoryStream ms = new MemoryStream())
                {
                    Content.InputStream.CopyTo(ms);
                    buffer = ms.GetBuffer();
                }
            return buffer;
        }
    }
}