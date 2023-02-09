using Library.Application.Interfaces.BusinessLogic;

namespace Library.Application.Interfaces
{
	public interface IUnitOfWork
	{
		public IAuthorizationRepository Authorization { get; set; }
		public IBookRepository Book { get; set; }
		public IUserRepository User { get; set; }
		public IRentedRepository Rented { get; set; }

	}
}
