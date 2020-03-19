using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.DTOs
{
	public class TipsForEveryOneDTO
	{
		public string Name { get; set; }
		public string Content { get; set; }
		public Guid UserId { get; set; }
	}
}
