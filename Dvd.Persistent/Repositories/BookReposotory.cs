using Library.Application.Interfaces.BusinessLogic;
using Library.Domain.Entity.Tables;
using Library.Persistent.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistent.Repositories
{
	public class BookReposotory : GenericRepository<Book>, IBookRepository
	{
		private new readonly Context _context;
		public BookReposotory(Context context) : base(context)
		{
			_context = context;
		}

		public async Task<int> LeaseArrange(int userId, int bookId)
		{
			User user = await _context!.Users!.FindAsync(userId);
			Book book = await _context!.Books!.FindAsync(bookId);

			DateTime now = DateTime.UtcNow;

			_ = await _context!.Renteds!.AddAsync(new Rented { User = user, Book = book, TakeTime = now.Date, DeliveryTime = now.AddDays(10).Date });
			if (book.Amount == 1)
			{
				book!.Amount -= 1;
			}
			if (book.Amount <= 1)
			{
				book.InStock = false;
				book!.Amount = 0;
			}

			_ = _context.SaveChanges();
			return 0;
		}

		public async Task<int> PostDifferent(ICollection<Book> books)
		{
			DbSet<Book>? c = _context.Books;
			List<Book> g = books.Where(f => !c.Select(b => b.Id).Contains(f.Id)).ToList();
			return 0;
		}

	}
}
