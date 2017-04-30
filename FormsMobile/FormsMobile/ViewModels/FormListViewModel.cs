using FormsMobile.Services;
using OpenNETCF.FormManager;
using OpenNETCF.IoC;
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
    class FormListViewModel : ViewModelBase
    {
        private IFormService m_svc;
        public FormSummary[] m_forms;
        private FormSummary m_selectedForm;
        private bool m_isRefreshing;

        public FormListViewModel()
        {
            Refresh();
        }

        public FormSummary[] AllForms
        {
            get { return m_forms; }
            set
            {
                m_forms = value;
                RaisePropertyChanged();
            }
        }

        public FormSummary SelectedForm
        {
            get { return m_selectedForm; }
            set
            {
                m_selectedForm = value;
                RaisePropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get { return m_isRefreshing; }
            set
            {
                if (value == IsRefreshing) return;
                m_isRefreshing = value;
                RaisePropertyChanged();
            }
        }

        public ICommand HandleRefresh
        {
            get
            {
                return new ValidatingCommand(() =>
                {
                    Refresh();
                });
            }
        }

        public void Refresh()
        {
            if (m_svc == null)
            {
                m_svc = RootWorkItem.Services.Get<IFormService>();
            }

            try
            {
                SelectedForm = null;
                AllForms = m_svc.GetFormSummaries();
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
