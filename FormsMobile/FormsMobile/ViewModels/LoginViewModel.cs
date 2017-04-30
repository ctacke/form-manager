using FormsMobile.Entities;
using OpenNETCF.MVVM;
using OpenNETCF.Windows.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FormsMobile.ViewModels
{
    class LoginViewModel : ViewModelBase
    {
        public string Username
        {
            get { return AppContext.CurrentUser; }
            set { AppContext.CurrentUser = value; }
        }

        public ICommand HandleLoginClicked
        {
            get
            {
                return new ValidatingCommand(async () =>
                {
                    // currently we have no user validation - we're just capturing the name
                    await NavigationService.ShowHome();
                });
            }
        }
    }
}
