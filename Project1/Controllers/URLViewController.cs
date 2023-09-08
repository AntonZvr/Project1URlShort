﻿using Microsoft.AspNetCore.Http;
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

        [HttpPost("addNewUrl")]
        public IActionResult Post([FromBody] URLViewModel newUrlVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _urlService.InsertNewURL(newUrlVM);

            return CreatedAtAction(nameof(GetById), new { id = newUrlVM.Id }, newUrlVM);
        }

        [HttpPost("shorten")]
        public IActionResult ShortenUrl([FromBody] string fullUrl)
        {
            if (string.IsNullOrWhiteSpace(fullUrl)) return BadRequest("URL is required");

            var shortened = _urlService.ShortenUrl(fullUrl);

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
