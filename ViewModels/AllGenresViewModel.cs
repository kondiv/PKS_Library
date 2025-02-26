using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class AllGenresViewModel : PageViewModel
    {
        public AllGenresViewModel()
        {
            PageName = Data.PageName.Genres;
        }
    }
}
