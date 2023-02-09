using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;

namespace Library.Persistent.Repositories
{
	internal class RentedRepository : GenericRepository<Rented>, IRentedRepository
	{
		public RentedRepository(Context context) : base(context)
		{
		}

	}
}
