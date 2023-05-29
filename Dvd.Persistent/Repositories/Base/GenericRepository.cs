using Library.Application.Interfaces.BusinessLogic.Base;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistent.Repositories.Base
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		internal readonly Context _context;
		public GenericRepository(Context context)
		{
			_context = context;

		}
		public async Task<T?> CreateAsync(T entity)
		{
			try
			{
				_ = await _context.Set<T>().AddAsync(entity);
				_ = await _context.SaveChangesAsync();
			}
			catch (Exception)
			{

			}

			return entity;
		}

		public async Task<T> CreateOrUpdateAsync(T entity)
		{
			if (_context.Set<T>().Contains(entity))
				await UpdateAsync(entity);
			else
				await CreateAsync(entity);
			await _context.SaveChangesAsync();
			return entity;
		}

		public async Task Delete(T entity)
		{
			_ = _context.Remove(entity);
			await _context.SaveChangesAsync();
		}

		public async Task<T?> FirstAsync()
		{
			return await _context.Set<T>().FirstOrDefaultAsync();
		}

		public virtual Task<List<T>> GetAllAsync()
		{
			DbSet<T> g = _context.Set<T>();
			g.Load();
			return g.ToListAsync();
		}
		public virtual async Task<T?> GetByIdAsync(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}

		public Task<T?> LastAsync()
		{
			throw new NotImplementedException();
		}

		public async Task UpdateAsync(T entity)
		{
			_context.Update<T>(entity);
			await _context.SaveChangesAsync();
		}
	}
}
