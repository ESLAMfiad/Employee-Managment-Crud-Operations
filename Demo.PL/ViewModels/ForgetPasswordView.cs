using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ForgetPasswordView
    {
        [Required(ErrorMessage = " Email required")]
        [EmailAddress(ErrorMessage = "invalid emal")]
        public string Email { get; set; }
    }
}
