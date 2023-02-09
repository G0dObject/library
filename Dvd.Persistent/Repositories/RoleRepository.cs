using Library.Application.Interfaces;
using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistent.Repositories
{
	internal class RoleRepository : GenericRepository<Role>, IRolesRepository
	{
		IContext _context;
		public RoleRepository(Context context) : base(context)
		{
			_context = context;
		}
		override public Task<List<Role>> GetAllAsync()
		{
			Task<List<Role>> result = _context.Roles.Include(c => c.Users).ToListAsync();
			return result;
		}
	}
}
