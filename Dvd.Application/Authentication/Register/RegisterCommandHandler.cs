using CQRS.Command;
using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;

namespace Library.Application.Authentication.Register
{
	public class RegisterCommandHandler : ICommandHandler<RegisterCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		public RegisterCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(RegisterCommand request)
		{
			User current = new()
			{
				UserName = request.UserName,
				Password = request.Password,
				Role = await _unitOfWork.Authorization.GetDefaultRole()
			};

			Task<User> result = _unitOfWork.Authorization.CreateAsync(current);
			return result.Id;
		}
	}
}
