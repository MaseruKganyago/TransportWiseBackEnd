using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstProject.Domain
{
	public class FileStorage
	{
		public Guid FileStorageId { get; set; }
		public string Name { get; set; }
		public string FilePath { get; set; }
		public string ContentType { get; set; }
	}
}
