using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="first name required")]
        public string FName { get; set; }

        [Required(ErrorMessage = "last name required")]
        public string LName { get; set; }

        [Required(ErrorMessage = " Email required")]
        [EmailAddress(ErrorMessage ="invalid emal")]
        public string Email { get; set; }
        [Required(ErrorMessage = " password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = " confirm password required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="password doesnt match")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }



    }
}
