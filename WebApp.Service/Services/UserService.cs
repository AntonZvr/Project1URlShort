using WebApp.Data.DAL.Repositories;
using WebApp.Data.DAL.RepositoryInterfaces;
using WebApplication1.DAL.Models;
using WebApplication1.ServiceInterfaces;
using WebApplication1.ViewModels;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Authenticate(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        var user = _userRepository.GetUserByUsernameAndPassword(username, password);

        if (user == null)
            return null;

        return user;
    }

    public User Register(RegisterViewModel model)
    {

        if (string.IsNullOrWhiteSpace(model.Password))
            throw new Exception("Password is required");

        if (_userRepository.IsUsernameTaken(model.Username))
            throw new Exception("Username \"" + model.Username + "\" is already taken");

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            Password = model.Password,
            Role = model.Role
        };

        _userRepository.AddUser(user);
        _userRepository.Save();

        return user;
    }
}
