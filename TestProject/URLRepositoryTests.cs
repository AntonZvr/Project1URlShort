using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.DAL.Repositories;
using WebApp.Data.DAL.Models;
using System.Linq;
using WebApplication1.DAL.Models;

namespace WebApp.Data.Tests.DAL.Repositories
{
    [TestClass]
    public class URLRepositoryTests
    {
        private AppDbContext _context;
        private URLRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repo = new URLRepository(_context);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public void GetURLs_ShouldReturnAllURLs()
        {
            // Arrange
            var url1 = new URLModel { FullUrl = "http://example.com", ShortUrl = "abc123" };
            var url2 = new URLModel { FullUrl = "http://example.org", ShortUrl = "def456" };
            _context.URLTable.AddRange(url1, url2);
            _context.SaveChanges();

            // Act
            var result = _repo.GetURLs();

            // Assert
            Assert.AreEqual(2, result.Count());
            CollectionAssert.Contains(result.ToList(), url1);
            CollectionAssert.Contains(result.ToList(), url2);
        }

        [TestMethod]
        public void GetURLById_ShouldReturnCorrectURL()
        {
            // Arrange
            var url = new URLModel { FullUrl = "http://example.com", ShortUrl = "abc123" };
            _context.URLTable.Add(url);
            _context.SaveChanges();

            // Act
            var result = _repo.GetURLById(url.Id);

            // Assert
            Assert.AreEqual(url, result);
        }

        [TestMethod]
        public void InsertNewURL_ShouldAddURLToContext()
        {
            // Arrange
            var url = new URLModel { FullUrl = "http://example.com", ShortUrl = "abc123" };

            // Act
            _repo.InsertNewURL(url);
            _repo.Save();

            // Assert
            Assert.AreEqual(1, _context.URLTable.Count());
            Assert.AreEqual(url, _context.URLTable.Single());
        }

        [TestMethod]
        public void GetURLByShortUrl_ShouldReturnCorrectURL()
        {
            // Arrange
            var url = new URLModel { FullUrl = "http://example.com", ShortUrl = "abc123" };
            _context.URLTable.Add(url);
            _context.SaveChanges();

            // Act
            var result = _repo.GetURLByShortUrl(url.ShortUrl);

            // Assert
            Assert.AreEqual(url, result);
        }
    }
}

