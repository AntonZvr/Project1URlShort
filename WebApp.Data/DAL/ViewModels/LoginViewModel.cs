using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels
{
    public class LoginViewModel
    {
        public string FirstName = "default";

        public string LastName = "default";
        [Required]
        public string LoginUsername { get; set; }
        [Required]
        public string LoginPassword { get; set; }
        public string Role { get; set; }
    }
}
