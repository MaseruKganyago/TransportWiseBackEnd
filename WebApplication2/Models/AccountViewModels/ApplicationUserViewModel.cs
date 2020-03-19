using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FirstProject.Models.AccountViewModels
{
	public class ApplicationUserViewModel
	{
		public string Id { get; set; }
		public object Email { get; set; }
		public string Name { get; set; }
		public string Surname { get;  set; }
		public string MobilePhone { get; set; }
		public IFormFile Photo { get; set; }
	}
}
