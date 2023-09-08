using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.DAL.Models;

namespace WebApp.Data.DAL.RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUserByUsernameAndPassword(string username, string password);
        bool IsUsernameTaken(string username);
        void AddUser(User user);
        void Save();

    }
}
