using Library.Application.Interfaces;
using Library.Application.Interfaces.BusinessLogic;
using Library.Persistent.Repositories;

namespace Library.Persistent
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly Context _context;
		private bool _disposed = false;
		public IAuthorizationRepository Authorization { get; set; }
		public IBookRepository Book { get; set; }
		public IRentedRepository Rented { get; set; }
		public IUserRepository User { get; set; }
		public IRolesRepository Role { get; set; }

		public UnitOfWork(Context context)
		{
			_context = context;
			Authorization = new AuthorizationRepository(context);
			Book = new BookReposotory(context);
			Rented = new RentedRepository(context);
			User = new UserRepository(context);
			Role = new RoleRepository(context);
		}

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
