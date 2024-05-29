using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = " Email required")]
        [EmailAddress(ErrorMessage = "invalid emal")]
        public string Email { get; set; }
        [Required(ErrorMessage = " password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}
