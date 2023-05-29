using Library.Application.Interfaces;
using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistent.Repositories
{
	internal class RentedRepository : GenericRepository<Rented>, IRentedRepository
	{
		private IContext _context;
		public RentedRepository(Context context) : base(context)
		{
			_context = context;
		}

		public async Task<Rented?> GetByBookId(int id)
		{
			return await _context!.Renteds.FirstOrDefaultAsync(f => f.Book.Id == id);
		}
	}
}
