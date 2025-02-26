using CommunityToolkit.Mvvm.ComponentModel;
using PKS_Library.Services.Interfaces;
using PKS_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Services.Realisations
{
    public partial class NavigationService : ObservableObject
    {
        [ObservableProperty]
        private ViewModelBase? _currentPage;

        public void NavigateTo(PageViewModel targetPage)
        {
            CurrentPage = targetPage;
        }
    }
}
