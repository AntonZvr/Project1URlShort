using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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

    public string ShortenUrl(string longUrl, int length = 7)
    {
        var token = GetUniqueKey(length);
        if (string.IsNullOrWhiteSpace(token)) return null;

        var shortUrl = new URLModel { FullUrl = longUrl, ShortUrl = token };

        _urlRepository.InsertNewURL(shortUrl);
        _urlRepository.Save();

        return $"https://localhost:44490/URLView/{token}";
    }

    public string ExpandUrl(string shortUrl)
    {
        var token = shortUrl.Split('/').LastOrDefault();
        var urlModel = _urlRepository.GetURLByShortUrl(token);

        return urlModel?.FullUrl;
    }

    private string GetUniqueKey(int size)
    {
        var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        var data = new byte[size];

        using (var crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetBytes(data);
        }

        var result = new StringBuilder(size);
        foreach (var b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }

        return result.ToString();
    }

}
