using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.DAL.Repositories;
using WebApplication1.DAL.Models;
using System.Linq;

namespace WebApp.Data.Tests.DAL.Repositories
{
    [TestClass]
    public class UserRepositoryTests
    {
        private AppDbContext _context;
        private UserRepository _repo;

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repo = new UserRepository(_context);
        }

        [TestMethod]
        public void GetUserByUsernameAndPassword_ShouldReturnUser()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password", 
                FirstName = "default", LastName = "default",
                Role = "user" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _repo.GetUserByUsernameAndPassword("testuser", "password");

            // Assert
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public void IsUsernameTaken_ShouldReturnTrueIfUsernameExists()
        {
            // Arrange
            var user = new User { Username = "existinguser", Password = "password",
                FirstName = "default", LastName = "default", Role = "user" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _repo.IsUsernameTaken("existinguser");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsUsernameTaken_ShouldReturnFalseIfUsernameDoesNotExist()
        {
            // Act
            var result = _repo.IsUsernameTaken("nonexistinguser");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddUser_ShouldAddUserToContext()
        {
            // Arrange
            var user = new User { Username = "newuser", Password = "password", 
                FirstName = "default", LastName = "default", Role = "user" };

            // Act
            _repo.AddUser(user);
            _repo.Save();

            // Assert
            Assert.AreEqual(1, _context.Users.Count());
            Assert.AreEqual(user, _context.Users.Single());
        }
    }
}
