using FormBuilder.Services;
using OpenNETCF.FormManager;
using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilder.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IFormStore m_store;

        protected IFormStore Store
        {
            get
            {
                if (m_store == null)
                {
                    m_store = RootWorkItem.Services.Get<IFormStore>();
                }

                return m_store;
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
