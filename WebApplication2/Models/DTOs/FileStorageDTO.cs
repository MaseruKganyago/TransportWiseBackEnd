using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Models.DTOs
{
	public class FileStorageDTO
	{
		public string Name { get; set; }
		public string FilePath { get; set; }
		public string ContentType { get; set; }
	}
}
