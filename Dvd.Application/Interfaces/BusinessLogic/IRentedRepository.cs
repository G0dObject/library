﻿using Library.Application.Interfaces.BusinessLogic.Base;
using Library.Domain.Entity.Tables;

namespace Library.Application.Interfaces.BusinessLogic
{
	public interface IRentedRepository : IGenericRepository<Rented>
	{
		public Task<Rented> GetByBookId(int id);
	}
}
