using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Domain
{
	public class Author
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string JobTitle { get; set; }
		public string DriverExperience { get; set; }
		//public List<Articles> Articles { get; } = new List<Articles>();
		//public List<FeedBack> FeedBack { get; } = new List<FeedBack>();

	}
}
