using CQRS.Query;
using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;

namespace Library.Application.Authentication.Login
{
	public class LoginQueryHandler : IQueryHandler<LoginQuery, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		public LoginQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(LoginQuery request)
		{
			List<User> users = await _unitOfWork.Authorization.GetAllAsync();

			User current = users.Where(f => f.UserName.Equals(request.UserName) & f.Password.Equals(request.Password)).FirstOrDefault() ?? new User() { Id = 0 };
			return current.Id;
		}
	}
}
