﻿using Library.Application.Interfaces;
using Library.Domain.Entity.Tables;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Brushes = System.Drawing.Brushes;
using Window = System.Windows.Window;

namespace Library.Client
{
	public partial class MainWindow : Window
	{
		private readonly IUnitOfWork _unitOfWork;
		private bool _adminbarvisibility = false;
		private bool _cartbarvisibility = true;
		private Role _role;
		private string _text;
		public MainWindow(IUnitOfWork unitOfWork, Role role)
		{
			_role = role;
			_unitOfWork = unitOfWork;
			InitializeComponent();
			LoadData();
			if (SideBar.Visibility == Visibility.Visible)
			{
				_adminbarvisibility = true;
			}

			if (role.Name == "Admin")
				Show.Visibility = Visibility.Visible;

			CartGrid.AutoGenerateColumns = true;

		}
		private void Application_Exit(object sender, ExitEventArgs e)
		{
			Environment.Exit(0);
		}

		private void LoadData_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			datagrid.Items.Clear();
			var disks = _unitOfWork.Disk.GetAllAsync();
			datagrid.AutoGenerateColumns = true;
			datagrid.BeginningEdit += (s, ss) => ss.Cancel = true;

			foreach (var item in disks.Result)
				datagrid.Items.Add(item);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

			for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
			{
				if (vis is DataGridRow)
				{
					var row = (DataGridRow)vis;
					var item = row.Item as Disk;

					CartGrid.Items.Add(item!);
				}
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			if (_adminbarvisibility)
			{
				SideBar.Visibility = Visibility.Hidden;
				_adminbarvisibility = false;

			}
			else
			{
				SideBar.Visibility = Visibility.Visible;
				_adminbarvisibility = true;
			}

		}

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			bool taken = Available.IsChecked ?? false;
			_unitOfWork.Disk.CreateAsync(new Disk() { Name = this.Name.Text, AgeCategory = AgeCategory.Text, IsTaken = taken });
			LoadData();
		}

		private void Cart_Click(object sender, RoutedEventArgs e)
		{
			if (_cartbarvisibility)
			{
				CartBar.Visibility = Visibility.Hidden;
				_cartbarvisibility = false;
			}
			else
			{
				CartBar.Visibility = Visibility.Visible;
				_cartbarvisibility = true;
			}
		}
		private void CreateDocument()
		{

			var path = $@"{_role.Name}\{DateTime.UtcNow.Month}\{DateTime.Now.Day}";
			Directory.CreateDirectory(path);
			path += @"\Film.txt";
			using (var writer = new StreamWriter(path))
			{
				foreach (var item in CartGrid.Items)
				{
					var disk = item as Disk;
					var str = $"{disk!.Name.ToString()} {disk.AgeCategory.ToString()} {disk.IsTaken.ToString()}";
					writer.WriteLine(str);
					_text += str;
				}
			}
			PrintDocument printDocument = new PrintDocument();
			printDocument.PrintPage += PrintPageHandler;

			PrintDialog printDialog = new PrintDialog();

			if (printDialog.ShowDialog() == true)
			{
				printDocument.Print();
			}

		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{


			CreateDocument();
		}


		void PrintPageHandler(object sender, PrintPageEventArgs e)
		{
			e.Graphics.DrawString(_text, new Font("Arial", 14), Brushes.Black, 0, 0);


		}
	}
}
