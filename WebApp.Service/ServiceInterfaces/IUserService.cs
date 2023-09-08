using WebApplication1.DAL.Models;

namespace WebApplication1.ServiceInterfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Register(User user);
    }
}
