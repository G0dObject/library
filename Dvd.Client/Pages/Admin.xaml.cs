using Client.Pages;
using Library.Domain.Entity.Tables;
using Library.Persistent;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Library.Client.Pages
{
	public partial class Admin : Window
	{
		private readonly Role _role;
		private int _tableindex = 0;
		private IEnumerable _datagridContext;
		private readonly UnitOfWork _unitOfWork;
		private readonly bool _initialize = false;

		private readonly object obj;
		public Admin(Role role, UnitOfWork unitOfWork)
		{
			_datagridContext = new List<object>();
			InitializeComponent();

			_unitOfWork = unitOfWork;
			_role = role;
			datagrid.AutoGenerateColumns = true;
			datagrid.ItemsSource = _datagridContext;
			_initialize = true;
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
					List<Rented> list = await _unitOfWork.Rented.GetAllAsync();
					_datagridContext = list;
					break;
				case 2:

					_datagridContext = await _unitOfWork.User.GetAllAsync();
					break;
				case 3:
					_datagridContext = (await _unitOfWork.Role.GetAllAsync()).Select(i => new { i.Id, i.Name });
					break;
				default:
					_datagridContext = await _unitOfWork.Book.GetAllAsync();
					break;
			}
			datagrid.ItemsSource = _datagridContext;
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
						Rented list = await _unitOfWork.Rented.CreateOrUpdateAsync(c as Rented);
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

	}
}
