using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.DAL.ViewModels;
using WebApplication1.ServiceInterfaces;

namespace Project1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class URLViewController : ControllerBase
    {
        private readonly IURLService _urlService;

        public URLViewController(IURLService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("getAllURLs")]
        public IActionResult GetURLs()
        {
            return Ok(_urlService.GetURLs());
        }

        [HttpGet("getUrlById/{id}", Name = "Get")]
        public IActionResult GetById(int id)
        {
            var urlById = _urlService.GetURLById(id);
            if (urlById == null)
            {
                return NotFound();
            }
            return Ok(urlById);
        }

        [HttpPost("insertShortenedUrl")]
        public IActionResult ShortenUrl([FromBody] URLViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FullUrl)) return BadRequest("URL is required");

            var shortened = _urlService.ShortenUrl(model.FullUrl);

            return string.IsNullOrEmpty(shortened) ? Problem("Unable to shorten the URL") : Ok(shortened);
        }

        [HttpGet("{shortUrl}")]
        public IActionResult RedirectShortUrl(string shortUrl)
        {
            if (string.IsNullOrWhiteSpace(shortUrl)) return BadRequest("Short URL is required");

            var fullUrl = _urlService.ExpandUrl(shortUrl);

            if (string.IsNullOrEmpty(fullUrl)) return NotFound();

            return Redirect(fullUrl);
        }
    }
}
