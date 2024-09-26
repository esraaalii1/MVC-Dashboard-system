global using DemoPresentationLayer.ViewModels;
using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Models;
using DemoPresentationLayer.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static DemoPresentationLayer.Utilities.Email;

namespace DemoPresentationLayer.Controllers
{
	public class AccountController : Controller
	{

		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signinManager;


		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
		{
			_userManager = userManager;
			_signinManager = signinManager;
		}

		public IActionResult Register()
		{
			return View();

		}
		[HttpPost]
		public IActionResult Register(RegisterViewModel registerViewModel)
		{
			if (!ModelState.IsValid) return View(registerViewModel);
			var user = new ApplicationUser
			{
				UserName = registerViewModel.UserName,
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				LastName = registerViewModel.LastName,

			};
			var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
			if (result.Succeeded) return RedirectToAction(nameof(Login));
			foreach (var error in result.Errors)
				ModelState.AddModelError(string.Empty, error.Description);
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(LoginViewModel loginViewModel)
		{
			if (!ModelState.IsValid) return View(loginViewModel);
			var user = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
			if (user != null)
			{
				if (_userManager.CheckPasswordAsync(user, loginViewModel.Password).Result)
				{
					var result = _signinManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, false).Result;
					if (result.Succeeded) return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", string.Empty));

				}
			}
			ModelState.AddModelError(string.Empty, "Incorrect Password or Email");
				return View(loginViewModel);
			}
		public new IActionResult SignOut()
		{
			_signinManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		public IActionResult ForgetPassword()=> View();

		[HttpPost]
		public IActionResult ForgetPassword(ForgetPasswordVM model)
		{
			if(!ModelState.IsValid) return View(model);

			var user= _userManager.FindByEmailAsync(model.Email).Result;
			if (user is not null)
			{
				var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
				var url = Url.Action(nameof(ResetPassword), nameof(AccountController).Replace("Controller", string.Empty), new {Email=model.Email,Token=token},Request.Scheme);
				var email = new Email
				{
					Subject = "Reset Password",
					Body = url!,
					Recipient = model.Email
				};
				MailSetting.SendEmail(email);
				return RedirectToAction(nameof(CheckYourInbox));
			}
			ModelState.AddModelError(string.Empty, "User Is Not Found");
			return View(model);
			
			
		}

		public IActionResult CheckYourInbox()
		{
			return View();
		}

		public IActionResult ResetPassword(string email,string token)
		{
			if (email is null || token is null) return BadRequest();
			TempData["Email"] = email;
			TempData["Token"] = token;	
			return View();
		}
		[HttpPost]
		public IActionResult ResetPassword(ResetPasswordVM model)
		{
			model.Email = TempData["Email"]?.ToString() ?? string.Empty;
			model.Token = TempData["Token"]?.ToString() ?? string.Empty;
			if(!ModelState.IsValid) return View(model);

			var user = _userManager.FindByEmailAsync(model.Email).Result;
			if(user != null)
			{
				var result =_userManager.ResetPasswordAsync(user , model.Token, model.Password).Result;
				if(result.Succeeded) return RedirectToAction(nameof(Login));

				foreach(var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);
			}
			ModelState.AddModelError(string.Empty, "User is Not Found");
			return View(model);

		}

	}
}
