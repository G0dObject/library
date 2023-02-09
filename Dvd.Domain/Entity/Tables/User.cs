using Library.Domain.Entity.Base;

namespace Library.Domain.Entity.Tables
{
	public class User : EntityBase
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public Role Role { get; set; }
		public int RoleId { get; set; }

		public override string ToString()
		{
			return base.Id.ToString();
		}
	}
}
