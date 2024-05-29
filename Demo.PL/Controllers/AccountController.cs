using demo.DAL.Models;
using Demo.PL.Helpers;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        #region Register
        //register
        //baseurl/account/register
        [HttpGet] //eldefault 
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)//server side valid
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    FName = model.FName,
                    LName = model.LName
                };
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                }
            }
            return View(model);
        }
        #endregion
        //login
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)//server side valid
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    //login
                    var Result = await _userManager.CheckPasswordAsync(User, model.Password);
                    if (Result)
                    {
                        //login
                        var LoginResult = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);
                        if (LoginResult.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                        ModelState.AddModelError(string.Empty, "password is incorrect");
                }
                else
                    ModelState.AddModelError(string.Empty, "email doesnt exist");

            }
            return View(model);
        }
        #endregion
        //signout
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        //forget pw
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendEmail(ForgetPasswordView model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email);
                if (User is not null)
                {
                    var token=await _userManager.GeneratePasswordResetTokenAsync(User); //valid for only one time for this user
                    //https://localhost:44320/account/resetpassword?email=eslamhossny@gmail.com?Token=
                    var ResetPasswordLink = Url.Action("ResetPassword", "Account", new {email=User.Email,Token=token},Request.Scheme); //req schema hya https://localhost:44320 kda bqqt dynamic
                    var email = new Email()
                    {
                        Subject = "reset password",
                        To = model.Email,
                        Body = ResetPasswordLink
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction(nameof(CheckInbox));
                }
                else
                    ModelState.AddModelError(string.Empty, "Email doesnt exist");

            }
            return View("ForgetPassword", model);


        }
        public IActionResult CheckInbox()
        {
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordView model)
        {
            if (ModelState.IsValid)
            {
                string email= TempData["email"] as string;
                string token= TempData["token"] as string;
                var User= await _userManager.FindByEmailAsync(email);
               var result= await _userManager.ResetPasswordAsync(User, token, model.NewPassword);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));
                else
                 foreach(var error in result.Errors)   
                        ModelState.AddModelError(string.Empty, error.Description);   
            }         
                return View(model);

        }
        //reset pw

    }
}
