using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Domain
{
	public class TipsForEveryOne
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public Guid UserId { get; set; }
		//public User User { get; set; }
	}
}
