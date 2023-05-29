using Client.Pages;
using Library.Domain.Entity.Base;
using Library.Domain.Entity.Tables;
using Library.Persistent;
using Microsoft.AspNetCore.Components;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace Library.Client.Pages
{
	public partial class Admin : Window
	{
		private readonly Role _role;
		private int _tableindex = 0;
		private IEnumerable<EntityBase> _datagridContext;
		private readonly UnitOfWork _unitOfWork;
		private readonly bool _initialize = false;
		private string _text;

		public Admin(Role role, UnitOfWork unitOfWork)
		{
			_datagridContext = new List<EntityBase>();
			InitializeComponent();

			_unitOfWork = unitOfWork;
			_role = role;
			datagrid.AutoGenerateColumns = true;
			datagrid.ItemsSource = _datagridContext;
			_initialize = true;
			LoadData(0);
			ManagerOff();
		}

		private void ManagerOff()
		{
			if (_role.Name != "Admin")
			{
				ComboRole.IsEnabled = false;
				ComboUser.IsEnabled = false;
			}
		}
		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ComboBox? cb = sender as ComboBox;
			_tableindex = cb!.SelectedIndex;
			if (_initialize)
			{
				LoadData(_tableindex);
			}
		}
		private async void LoadData(int index)
		{
			switch (index)
			{
				case 0:
					_datagridContext = await _unitOfWork.Book.GetAllAsync();
					break;
				case 1:
					_datagridContext = await _unitOfWork.Rented.GetAllAsync();
					break;
				case 2:
					_datagridContext = await _unitOfWork.User.GetAllAsync();
					break;
				case 3:
					_datagridContext = await _unitOfWork.Role.GetAllAsync();
					break;
				default:
					_datagridContext = await _unitOfWork.Book.GetAllAsync();
					break;
			}
			datagrid.ItemsSource = _datagridContext;
		}

		private void Button_Click_6(object sender, RoutedEventArgs e)
		{
			foreach (object? item in _datagridContext)
			{

				switch (_tableindex)
				{
					case 0:
						Book? Book = item as Book;
						string strb = $"Название = {Book!.Name};\nОписание = {Book.Description};\nКоличество = {Book.Amount};\n\n";
						_text += strb;
						break;
					case 1:
						Rented? Rented = item as Rented;
						string strr = $"Номер пользователя = {Rented.User.Id};\nНомер книги = {Rented.Book.Id};\nВремя выдачи = {Rented.TakeTime.ToString("yyyy:MM:dd")};\nВремя сдачи = {Rented.DeliveryTime.ToString("yyyy:MM:dd")};\n\n";
						_text += strr;
						break;
					case 2:
						User? User = item as User;
						string stru = $"Номер пользователя = {User!.Id};\nЛогин пользователя = {User.UserName};\n";
						_text += stru;
						break;
					case 3:
						Role? Role = item as Role;
						string strro = $"Номер роли = {Role!.Id};\nИмя роли = {Role!.Name};\n\n";
						_text += strro;
						break;
				}
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
		public void datagrid_RowEditEnding_1(object sender, DataGridRowEditEndingEventArgs e)
		{
			_ = Dispatcher.InvokeAsync(new Action(async () =>
			{
				object c = e.Row.Item;
				switch (_tableindex)
				{
					case 0:
						_ = await _unitOfWork.Book.CreateOrUpdateAsync(c as Book);
						break;
					case 1:
						_ = await _unitOfWork.Rented.CreateOrUpdateAsync(c as Rented);
						break;
					case 2:
						_ = await _unitOfWork.User.CreateOrUpdateAsync(c as User);
						break;
					case 3:
						_ = await _unitOfWork.Role.CreateOrUpdateAsync(c as Role);
						break;
				}
				LoadData(_tableindex);
			}), DispatcherPriority.ContextIdle);
		}

		private void datagrid_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Delete)
			{
				foreach (object row in datagrid.SelectedItems)
				{
					switch (_tableindex)
					{
						case 0:
							_ = _unitOfWork.Book.Delete(row as Book);
							break;
						case 1:
							_ = _unitOfWork.Rented.Delete(row as Rented);
							break;
						case 2:
							_ = _unitOfWork.User.Delete(row as User);
							break;
						case 3:
							_ = _unitOfWork.Role.Delete(row as Role);
							break;
					}
				}
				LoadData(_tableindex);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			new Authentication().Show();
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			LoadData(_tableindex);
			try
			{
				switch (_tableindex)
				{
					case 0:
						datagrid.ItemsSource = (_datagridContext as IEnumerable<Book>).Where(f => f.Name.ToLower().StartsWith(SearchField.Text.ToLower())).ToList();
						break;
					case 1:
						datagrid.ItemsSource = (_datagridContext as IEnumerable<Rented>).Where(f => f.User.Id.ToString().ToLower().StartsWith(SearchField.Text.ToLower())).ToList();
						break;
					case 2:
						datagrid.ItemsSource = (_datagridContext as IEnumerable<User>).Where(f => f.Id.ToString().StartsWith(SearchField.Text.ToLower())).ToList();
						break;
					case 3:
						datagrid.ItemsSource = (_datagridContext as IEnumerable<Role>).Where(f => f.Name.ToLower().StartsWith(SearchField.Text.ToLower())).ToList();
						break;
					default:
						break;
				}
			}
			finally { }

			//_datagridContext.Where()
		}

		private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			Helper.Open(e);
		}



	}
}
