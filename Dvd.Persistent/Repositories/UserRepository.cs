using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;

namespace Library.Persistent.Repositories
{
	internal class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(Context context) : base(context)
		{
		}
	}
}
