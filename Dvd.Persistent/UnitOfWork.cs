using Library.Application.Interfaces;
using Library.Application.Interfaces.BusinessLogic;
using Library.Persistent.Repositories;

namespace Library.Persistent
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private Context _context;
		private bool _disposed = false;
		public UnitOfWork(Context context)
		{
			_context = context;
			Authorization = new AuthorizationRepository(context);
			Disk = new DiskReposotory(context);
		}

		public IAuthorizationRepository Authorization { get; set; }
		public IDiskRepository Disk { get; set; }
		public virtual void Dispose()
		{
			if (!_disposed)
			{
				_context.Dispose();
				_disposed = true;
			}
		}
	}
}
