using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.DAL.Models;
using WebApp.Data.DAL.ViewModels;

namespace WebApplication1.ServiceInterfaces
{
    public interface IURLService
    {
        IEnumerable<URLModel> GetURLs();
        URLModel GetURLById(int id);
        void InsertNewURL(URLViewModel UrlVM);
    }
}
