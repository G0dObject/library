using Library.Application.Authentication.Login;
using Library.Application.Authentication.Register;
using Library.Client.Model;
using Library.Client.Pages;
using Library.Domain.Entity.Tables;
using Library.Persistent;
using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;

namespace Client.Pages
{
	public partial class Authentication : Window
	{
		private readonly Context _context;
		private readonly UnitOfWork _unitOfWork;
		private bool _isRegister = true;
		private Role _role = new();
		private int _userid;
		private readonly RegisterModel command;
		public Authentication()
		{
			command = new RegisterModel();

			_context = new Context(new DbContextOptions<Context>());
			_unitOfWork = new UnitOfWork(_context);
			System.Threading.Tasks.Task res = _unitOfWork.Authorization.CreateAdmin();
			InitializeComponent();
			DataContext = command;
		}

		private async void RegistrationButton(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(command.Error))
			{
				try
				{
					RegisterCommand registerCommand = new(RUsername.Text, RPassword.Text, new Role() { Name = "User" });
					RegisterCommandHandler handler = new(_unitOfWork);

					if (await _unitOfWork.Authorization.Exist(registerCommand.UserName!))
					{
						_ = MessageBox.Show("user already registered");
						return;
					}
					_userid = await handler.Handle(registerCommand);

					Ok();
				}
				catch (Exception)
				{
					throw;
				}
			}
		}
		private async void LoginButton(object sender, RoutedEventArgs e)
		{
			try
			{
				LoginQuery loginQuery = new(LUsername.Text, LPassword.Text);
				LoginQueryHandler handler = new(_unitOfWork);

				int result = await handler.Handle(loginQuery);
				_userid = result;
				_role = await _unitOfWork.Authorization.GetRole(result);
				Ok();
			}
			catch (Exception ex)
			{
				_ = MessageBox.Show("Что-то не так, попробуйте снова", ex.Message);
			}
		}
		private void SwapMode(object sender, RoutedEventArgs e)
		{
			if (_isRegister)
			{
				Login.Visibility = Visibility.Visible;
				Register.Visibility = Visibility.Hidden;

				SwapButton.Content = "Регистрация";
				_isRegister = false;
			}
			else
			{
				Login.Visibility = Visibility.Hidden;
				Register.Visibility = Visibility.Visible;

				SwapButton.Content = "Войти";
				_isRegister = true;
			}
		}
		private void Ok()
		{
			Hide();
			Window mainWindow;

			if (_role.Name == "Admin" | _role.Name == "Manager")
			{
				mainWindow = new Admin(_role, _unitOfWork);
			}
			else if (_role.Name == "User")
			{
				mainWindow = new Reader(_unitOfWork, _userid);
			}
			else
			{
				throw new Exception();
			}

			mainWindow.Show();
			Close();
		}
	}
}


