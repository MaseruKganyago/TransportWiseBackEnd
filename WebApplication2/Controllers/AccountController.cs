using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FirstProject.Models.PasswordRecovery;
using FirstProject.Domain;
using FirstProject.Models.AccountViewModels;
using FirstProject.Models.ManageViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using FirstProject.Controllers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace SimpleNotes.Api.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AccountController : ControllerBase
	{


		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IHostingEnvironment _hostingEnvironment;
		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			  IHostingEnvironment hostingEnvironment)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_hostingEnvironment = hostingEnvironment;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("Login")]
	//	[SwaggerResponse(200, typeof(Token))]
		[Authorize]
		public async Task<ActionResult> Login(LoginViewModel model)
		{
			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

			if (result.Succeeded)
			{
				var user = await _userManager.FindByNameAsync(model.Email);
				TokenClaimHandler(user, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token);
				var userModel = new
				{
					user.Id,
					user.Name,
					user.Email,
					user.Surname,
					user.MobilePhone,
					user.ProfileImage
				};

				return Ok(new
				{
					userToken = tokenHandler.WriteToken(token),
					userInfo = userModel,
					//success = true
				});
			}
			return Unauthorized();
		}

		private void TokenClaimHandler(ApplicationUser userFromRepo, out JwtSecurityTokenHandler tokenHandler, out SecurityToken token)
		{
			var claims = new[]
						   {
				new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id),
				new Claim(ClaimTypes.Name, ($"{userFromRepo.UserName}"))
				};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("630A453A-CDD2-4AF9-8028-133FC4CC6A6E75D5E7E8-9E3E-464A-8A74-982F5854D1A8"));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			tokenHandler = new JwtSecurityTokenHandler();
			token = tokenHandler.CreateToken(tokenDescriptor);
		}

		[HttpPost]
		[Route("Logout")]
		[Authorize]
		public async Task<ActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			HttpContext.Session.Clear();
			return Ok();
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("Register")]
		[Authorize]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			try
			{

			}
			catch (Exception)
			{

				throw;
			}
			if (await _userManager.FindByNameAsync(model.Email) != null)
			{
				ModelState.AddModelError(nameof(model.Email), $"A user named '{model.Email}' already exists");
				return BadRequest(ModelState);
			}

			ApplicationUser applicationUser = new ApplicationUser { UserName = model.Email, MobilePhone = model.MobilePhone, Name = model.Name, Surname = model.Surname, Email = model.Email };
			var result = await _userManager.CreateAsync(applicationUser, model.Password);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return BadRequest(ModelState);
			}

			// Get user key from password
			applicationUser = await _userManager.FindByNameAsync(model.Email);
			await _userManager.UpdateAsync(applicationUser);
			return Ok(
				applicationUser.Email
				);
		}

		[HttpPut]
		[Route("UpdateUser")]
		[AllowAnonymous]
		public async Task<IActionResult> EditUser(ApplicationUserViewModel model)
		{
			var user = await _userManager.FindByIdAsync(model.Id);

			if (user == null)
			{
					ModelState.AddModelError(nameof(model.Email), $"User with Id = {model.Id} cannot be found");
					return BadRequest(ModelState);
			}
			else
			{

					user.Name = model.Name;
					user.Surname = model.Surname;
					user.MobilePhone = model.MobilePhone;
			

				var result = await _userManager.UpdateAsync(user);

				if (!result.Succeeded)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
					return BadRequest(ModelState);
				}
			}
			var userModel = new
				{
					user.Name,
					user.Surname,
					user.MobilePhone,
					user.ProfileImage
				};
			return Ok(new
			{
				userInfo = userModel
			});
				
		}

		[HttpPut]
		[Route("Password")]
		[Authorize]
		public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			var applicationUser = await _userManager.GetUserAsync(User);
			var result = await _userManager.ChangePasswordAsync(applicationUser, model.NewPassword, model.ConfirmPassword);
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return BadRequest(ModelState);
			}

			return Ok();
		}

		[HttpGet]
		[Route("Authenticated")]
		[Authorize]
		public async Task<ActionResult> IsAuthenticated()
		{
			ApplicationUser user = await Current();
			return Ok(user);
		}

		protected async Task<ApplicationUser> Current() => await _userManager.FindByNameAsync(User.Identity.Name);

		[Route("ForgotPassword")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
				// Find the user by email
				var user = await _userManager.FindByNameAsync(model.Email);
				// If the user is found AND Email is confirmed
				if (user == null )
				{
					return BadRequest("invalid email");
				}

			// Generate the reset password token
			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			// Build the password reset link
			var passwordResetLink = Url.Action("ResetPassword", "Account",
					new { Email = model.Email, Token = token }, Request.Scheme);

			// Log the password reset link
			//Logger.Log(LogLevel.Warning, passwordResetLink);

			// Send the user to Forgot Password Confirmation view
			return Ok(new TokenForget
			{
				Token = token,
				Link = passwordResetLink
			});
		}

		[Route("ResetPassword")]
		[HttpPost]
		[AllowAnonymous]
		[Authorize]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				// Find the user by email
				var user = await _userManager.FindByNameAsync(model.Email);

				if (user != null)
				{
					// reset the user password
					var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
					if (result.Succeeded)
					{
						return Ok();
					}
					// Display validation errors. For example, password reset token already
					// used to change the password or password complexity rules not met
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
					return Ok(model);
				}

				// To avoid account enumeration and brute force attacks, don't
				// reveal that the user does not exist
				return Ok();
			}
			// Display validation errors if model state is not valid
			return Ok(model);
		}
	}
}

namespace FirstProject.Controllers
{
	class Token
	{
		public string UserToken { get; set; }
	}

	class TokenForget
	{
		public string Token { get; set; }
		public string Link { get; set; }
	}
}