using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.DAL.Models;
using WebApp.Data.DAL.RepositoryInterfaces;
using WebApplication1.DAL.Models;

namespace WebApp.Data.DAL.Repositories
{
    public class URLRepository : IURLRepository
    {
        private readonly AppDbContext _context;

        public URLRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<URLModel> GetURLs()
        {
            return _context.URLTable.AsQueryable();
        }

        public URLModel GetURLById(int Id)
        {
            return _context.URLTable.Find(Id);
        }

        public void InsertNewURL(URLModel UrlModel)
        {
            _context.URLTable.Add(UrlModel);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
