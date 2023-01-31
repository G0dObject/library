using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;

namespace Library.Persistent.Repositories
{
	public class DiskReposotory : GenericRepository<Disk>, IDiskRepository
	{
		private new readonly Context _context;
		public DiskReposotory(Context context) : base(context)
		{
			_context = context;
		}

	}
}
