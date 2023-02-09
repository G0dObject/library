

using Client.Pages;
using Library.Application.Books.Lease;
using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Library.Client.Pages
{
	public partial class Reader : Window
	{
		private readonly IUnitOfWork _unitOfWork;
		private string _text = "";
		private readonly int _userid;
		public Reader(IUnitOfWork unitOfWork, int userid)
		{
			_unitOfWork = unitOfWork;
			InitializeComponent();
			LoadData();
			_userid = userid;
		}
		private void LoadData()
		{
			datagrid.Items.Clear();
			CartGrid.Items.Clear();

			System.Threading.Tasks.Task<System.Collections.Generic.List<Book>> Books = _unitOfWork.Book.GetAllAsync();
			datagrid.AutoGenerateColumns = true;
			datagrid.BeginningEdit += (s, ss) => ss.Cancel = true;

			Books.Result.ForEach(item => datagrid.Items.Add(item));
		}
		private void LoadData_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			for (Visual? vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
			{
				if (vis is DataGridRow row)
				{
					Book? item = row.Item as Book;

					_ = CartGrid.Items.Add(item!);
					Button? sender1 = sender as Button;
					sender1!.IsEnabled = false;
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			CreateDocument();
		}

		private void CreateDocument()
		{
			foreach (object? item in CartGrid.Items)
			{
				Book? Book = item as Book;
				string str = $"{Book!.Name} {Book.Description} {Book.InStock}";
				_text += str;
			}
			PrintDocument printDocument = new();
			printDocument.PrintPage += PrintPageHandler;

			PrintDialog printDialog = new();

			if (printDialog.ShowDialog() == true)
			{
				printDocument.Print();
			}
		}

		private void PrintPageHandler(object sender, PrintPageEventArgs e)
		{
			e!.Graphics!.DrawString(_text, new Font("Arial", 14), System.Drawing.Brushes.Black, 0, 0);
		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			LeaseArrangeCommandHandler leaseArrangeCommandHandler = new(_unitOfWork);
			foreach (Book item in CartGrid.Items)
			{
				LeaseArrangeCommand request = new() { UserId = _userid, BookId = item.Id };
				_ = leaseArrangeCommandHandler.Handle(request);
			}
			LoadData();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
			new Authentication().Show();
			this.Close();
		}
	}
}
