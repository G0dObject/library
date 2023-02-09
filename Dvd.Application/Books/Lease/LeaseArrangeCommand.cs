using CQRS.Command;

namespace Library.Application.Books.Lease
{
	public class LeaseArrangeCommand : ICommand<int>
	{
		public int BookId { get; set; }
		public int UserId { get; set; }
	}
}
