using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library.Client.Model
{
	public class BookModel : INotifyPropertyChanged
	{
		public string? Id
		{
			get => Id; set
			{
				Id = value;
				OnPropertyChanged(nameof(Id));
			}
		}
		public string? Name
		{
			get => Name; set
			{
				Name = value;
				OnPropertyChanged(nameof(Name));
			}
		}
		public string? Description
		{
			get => Description; set
			{
				Description = value;
				OnPropertyChanged(nameof(Description));
			}
		}
		public bool InStock
		{
			get => InStock;
			set
			{
				InStock = value;
				OnPropertyChanged(nameof(InStock));
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
