using WebApplication1.DAL.Models;
using WebApplication1.ServiceInterfaces;

public class UserService : IUserService
{
    private AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public User Authenticate(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        var user = _context.Users.SingleOrDefault(x => x.Username == username && x.Password == password);

        // Check if username exists and password is correct
        if (user == null)
            return null;

        // Authentication successful
        return user;
    }

    public User Register(User user)
    {
        // Validation

        if (string.IsNullOrWhiteSpace(user.Password))
            throw new Exception("Password is required");

        if (_context.Users.Any(x => x.Username == user.Username))
            throw new Exception("Username \"" + user.Username + "\" is already taken");

        _context.Users.Add(user);
        _context.SaveChanges();

        return user;
    }
}
