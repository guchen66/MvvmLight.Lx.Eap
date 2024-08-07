using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Dtos
{
    public class LoginDto: ObservableObject
    {
		private string _username;

		public string Username
		{
			get => _username;
			set => SetProperty(ref _username, value);
		}

		private string _password;

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		private bool _isSelected;

		public bool IsSelected
        {
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

	}
}
