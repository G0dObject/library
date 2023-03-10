using Library.Application.Interfaces.BusinessLogic.Base;
using Library.Domain.Entity.Tables;

namespace Library.Application.Interfaces.BusinessLogic
{
	public interface IAuthorizationRepository : IGenericRepository<User>
	{
		public Task<Role> GetDefaultRole();
		public Task<Role> GetRole(int id);
		public Task CreateAdmin();
		public Task CreateManager();
		public Task<bool> Exist(string name);
	}
}
