﻿using Library.Domain.Entity.Base;

namespace Library.Domain.Entity.Tables
{
	public class Rented : EntityBase
	{
		public DateTime TakeTime { get; set; }
		public DateTime DeliveryTime { get; set; }
		public User User { get; set; }
		public Book Book { get; set; }

		public override string ToString()
		{
			return base.ToString();
		}
	}
}
