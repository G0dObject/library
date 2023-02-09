using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Client.Model.Present.Extensions
{
	internal static class RentedMaper
	{
		internal static ICollection<RentedPresent> MapToPresent(this ICollection<Rented> renteds)
		{
			List<RentedPresent> ret = new List<RentedPresent>();
			foreach (Rented item in renteds)
			{
				ret.Add(new RentedPresent() { BookId = item.Id, DeliveryTime = item.DeliveryTime, Id = item.Id, TakeTime = item.TakeTime, UserId = item.User.Id });
			}
			return ret;
		}
		internal static Rented MapToTable(this RentedPresent item)
		{
			Rented result = new Rented()
			{
				Id = item.Id,
				TakeTime = item.TakeTime,
				DeliveryTime = item.DeliveryTime
			};

			return result;
		}

	}
}
