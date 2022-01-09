using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPS.AutoFileTransform
{
	public class TransformationInfo
	{
		public TransformationInfo()
		{
		}

		public List<FileSet> MyProperty { get; private set; } = new();

		public record class FileSet
		{
			public string RoodDir { get; set; }
			public Regex FilePattern { get; set; }
			public bool IncludeSubdirectories { get; set; } = true;
		}
	}
}
