using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordView
    {
        [Required(ErrorMessage ="new password is required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="password doesnt match")]
        public string ConfirmPassword { get; set; }

    }
}
