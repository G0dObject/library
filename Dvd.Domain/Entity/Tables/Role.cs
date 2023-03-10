using Library.Domain.Entity.Base;

namespace Library.Domain.Entity.Tables
{
	public class Role : EntityBase
	{
		public string Name { get; set; }
		public virtual ICollection<User> Users { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}
