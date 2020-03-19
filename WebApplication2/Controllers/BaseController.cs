using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FirstProject.Data;
using FirstProject.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
	public class BaseController : ControllerBase
	{
		protected MyDBContext _basecontext { get; }
		protected UserManager<ApplicationUser> _userManager;
		public BaseController(MyDBContext context, UserManager<ApplicationUser> userManager)
		{
			_basecontext = context;
			_userManager = userManager;
		}


		protected async Task<ApplicationUser> CurrentUser() => await _userManager.FindByNameAsync(User.Identity.Name);

		protected bool FileExists(string path)
		{
			return System.IO.File.Exists(RelativeFilePath(path));
		}

		protected void SaveFile(string file, string path)
		{
			using(var stream = System.IO.File.Create(RelativeFilePath(path)))
			{
				stream.Write(Convert.FromBase64String(file));
			}
		}

		private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
		{
			return attributes & ~attributesToRemove;
		}

		protected FileResult GetPic(string path, string contentType)
		{
			return File(System.IO.File.ReadAllBytes(RelativeFilePath(path)), contentType);
		}
		protected virtual string RelativeFilePath(string path)
		{
			return path;
		}
	}
}
