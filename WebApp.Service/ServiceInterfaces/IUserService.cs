using WebApplication1.DAL.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.ServiceInterfaces
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        User Register(RegisterViewModel model);
    }
}
