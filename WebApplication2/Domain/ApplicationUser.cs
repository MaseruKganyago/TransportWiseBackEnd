using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FirstProject.Domain

{
	public class ApplicationUser : IdentityUser
	{

		[Required]
		public string Name { get; set; }

		[Required]
		public string Surname { get; set; }

		[Phone]
		public string MobilePhone { get; set; }
	
		public string ProfileImage { get; set; }
	}

}
