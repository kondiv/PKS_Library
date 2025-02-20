using CommunityToolkit.Mvvm.ComponentModel;
using PKS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class PageViewModel : ViewModelBase
    {
        [ObservableProperty]
        private PageName _pageName;
    }
}
