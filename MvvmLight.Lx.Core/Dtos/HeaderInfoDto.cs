using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight.Lx.Core.Dtos
{
    public class HeaderInfoDto: ObservableObject
    {
		private string _headerName;

		public string HeaderName
		{
			get => _headerName;
			set => SetProperty(ref _headerName, value);
		}

	}
}
