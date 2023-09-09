using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Data.DAL.Models
{
    public class URLModel
    {
        public int Id { get; set; }
        public string FullUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
