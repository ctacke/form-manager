using OpenNETCF.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsMobile.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            FormList = new FormListViewModel();
        }

        public FormListViewModel FormList { get; set; }
    }
}
