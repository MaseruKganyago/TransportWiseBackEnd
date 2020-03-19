using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Domain
{
	public class FuelWise
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public Guid AuthorId { get; set; }
		//public Author Author { get; set; }
	}
}
