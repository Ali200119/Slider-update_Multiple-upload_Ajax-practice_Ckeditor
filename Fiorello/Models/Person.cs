using System;
namespace Fiorello.Models
{
	public class Person: BaseEntity
	{
		public string Image { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public int ExpertId { get; set; }
		public Expert Expert { get; set; }
	}
}