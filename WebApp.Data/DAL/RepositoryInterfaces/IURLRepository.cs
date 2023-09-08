using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.DAL.Models;

namespace WebApp.Data.DAL.RepositoryInterfaces
{
    public interface IURLRepository
    {
        IQueryable<URLModel> GetURLs();
        URLModel GetURLById(int Id);
        void InsertNewURL(URLModel UrlModel);
        URLModel GetURLByShortUrl(string shortUrl);
        void Save();
    }
}
