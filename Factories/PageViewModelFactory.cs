using PKS_Library.Data;
using PKS_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.Factories
{
    public class PageViewModelFactory(Func<PageName, PageViewModel> PageFactory)
    { 
        public PageViewModel GetPageViewModel(PageName pageName) => PageFactory.Invoke(pageName);
    }
}
