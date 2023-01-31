using Library.Application.Interfaces.BusinessLogic;

namespace Library.Application.Interfaces
{
	public interface IUnitOfWork
	{
		public IAuthorizationRepository Authorization { get; set; }
		public IDiskRepository Disk { get; set; }
	}
}
