using System;
using System.ComponentModel;

namespace Library.Client.Model.Present
{
	internal class RentedPresent
	{
		public int Id { get; set; }
		public DateTime TakeTime { get; set; }
		public DateTime DeliveryTime { get; set; }
		public int UserId { get; set; }
		public int BookId { get; set; }
	}
}
