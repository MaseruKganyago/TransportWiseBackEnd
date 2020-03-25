using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FirstProject.Domain;
using FirstProject.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using FirstProject.Models;

namespace FirstProject.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[System.Web.Http.HostAuthentication(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalBearer)]
	public class ImagesController : BaseController
	{
		private readonly MyDBContext _context;
		public ImagesController(MyDBContext context, UserManager<ApplicationUser> userManager) : base(context, userManager)
		{
			_context = context;
			_userManager = userManager;

		}

		private static readonly string BASE_FILE_PATH = "App_Data/Files";

		[HttpPost]
		[System.Web.Http.HostAuthentication(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalBearer)]
		public async Task<IActionResult> SetProfile([FromBody]  Encoded image)
		{
			var user = await _userManager.FindByIdAsync(image.Id);
			if (image.Name == null)
			{
				return ErrorNote("ProfilePicture of type image is required");
			}

			SaveFile(image.Data, $"{image.Name}{".PNG"}");

			var pic = new FileStorage
			{
				Name = $"{Path.GetFileName(image.Name)}{".PNG"}",
				FilePath = $"{Path.GetFileName(image.Name)}{".PNG"}",
				ContentType = image.Type,
			};

			user.ProfileImage = $"{Path.GetFileName(image.Name)}{".PNG"}";
			var result = await _userManager.UpdateAsync(user);
			_context.FileStorage.Add(pic);
			_context.SaveChanges();
			return Ok();
		}

		[HttpGet]
		[System.Web.Http.HostAuthentication(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalBearer)]
		public ActionResult GetProfile(string Name)
		{
			if (FileExists(Name))
			{
				return GetPic(Name, "image/jpeg");
			}
			return NotFound("Error fetching photo");
		}

		private ActionResult ErrorNote(string v)
		{
			throw new NotImplementedException();
		}

		protected override string RelativeFilePath(string path)
		{
			return base.RelativeFilePath($"{ BASE_FILE_PATH }{ path }");
		}

	}
}