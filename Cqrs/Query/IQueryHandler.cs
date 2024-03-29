﻿using Cqrs.Command;

namespace Cqrs.Query
{
	public interface IQueryHandler<in TRequest, TQuery> where TRequest : IQuery<TQuery>
	{
		public Task<TQuery> Handle(TRequest request);
	}
}
