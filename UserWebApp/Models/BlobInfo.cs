using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UserWebApp.Models
{
    public class BlobInfo
    {
        public Stream Content { get; set; }
        public string ContentType { get; set; }

        public BlobInfo(Stream Content, string ContentType)
        {
            this.Content = Content;
            this.ContentType = ContentType;
        }
    }
}
