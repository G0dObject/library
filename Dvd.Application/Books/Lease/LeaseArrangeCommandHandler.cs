using CQRS.Command;
using Library.Application.Interfaces;

namespace Library.Application.Books.Lease
{
	public class LeaseArrangeCommandHandler : ICommandHandler<LeaseArrangeCommand, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		public LeaseArrangeCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public Task<int> Handle(LeaseArrangeCommand request)
		{
			return _unitOfWork.Book.LeaseArrange(request.UserId, request.BookId);

		}
	}
}
