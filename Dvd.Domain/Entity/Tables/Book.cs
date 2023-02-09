using Library.Domain.Entity.Base;

namespace Library.Domain.Entity.Tables
{
	public class Book : EntityBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool InStock { get; set; }


		public override string ToString()
		{
			return base.Id.ToString();
		}
	}
}