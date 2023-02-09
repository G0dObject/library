using Library.Application.Interfaces.BusinessLogic.Base;
using Library.Domain.Entity.Tables;

namespace Library.Application.Interfaces.BusinessLogic
{
	public interface IBookRepository : IGenericRepository<Book>
	{
		public Task<int> LeaseArrange(int userId, int bookId);
		public Task<int> PostDifferent(ICollection<Book> books);
	}
}
