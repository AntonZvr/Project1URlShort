using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Data.DAL.Models;
using WebApp.Data.DAL.Repositories;
using WebApp.Data.DAL.RepositoryInterfaces;
using WebApp.Data.DAL.ViewModels;
using WebApplication1.ServiceInterfaces;

public class URLsService : IURLService
    {
        private readonly IURLRepository _urlRepository;

        public URLsService(IURLRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public IEnumerable<URLModel> GetURLs()
        {
            return _urlRepository.GetURLs();
        }

        public URLModel GetURLById(int id)
        {
            return _urlRepository.GetURLById(id);
        }

        public void InsertNewURL(URLViewModel UrlVM)
        {
            try
            {
                var newUrl = new URLModel
                {
                    FullUrl = UrlVM.FullUrl,
                    ShortUrl = UrlVM.ShortUrl
                };

                _urlRepository.InsertNewURL(newUrl);
                _urlRepository.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while inserting the URL: " + ex.Message);
            }
        }
    }
