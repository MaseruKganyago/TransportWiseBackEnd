﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Domain
{
	public class Articles
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public string UserName { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
	}
}