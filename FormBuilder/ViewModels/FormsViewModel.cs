using FormBuilder.Services;
using OpenNETCF.FormManager;
using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FormBuilder.ViewModels
{
    class FormsViewModel : ViewModelBase
    {
        private FormSummary[] m_forms;
        private Form m_selectedForm;
        private FormSummary m_selectedSummary;

        public FormsViewModel()
        {
#pragma warning disable CS4014
            Refresh();
#pragma warning restore
        }

        async Task Refresh()
        {
            try
            {
                var f = await Store.GetFormSummariesAsync();
                Forms = f.ToArray();
            }
            catch (Exception ex)
            {
                // TODO: raise an error, etc. for the UI
                // this is most likely going to be a "cannot reach the server" kind of thing, but needs testing
                Debug.WriteLine(ex);
            }
        }

        public FormSummary[] Forms
        {
            get { return m_forms; }
            set
            {
                m_forms = value;
                RaisePropertyChanged();
            }
        }

        public Form SelectedForm
        {
            get { return m_selectedForm; }
            set
            {
                m_selectedForm = value;
                RaisePropertyChanged();
            }
        }

        public FormSummary SelectedSummary
        {
            get { return m_selectedSummary; }
            set
            {
                m_selectedSummary = value;
                RaisePropertyChanged();

                Task.Run(async () =>
                {
                    SelectedForm = await GetFormForSummary(SelectedSummary);
                });
            }
        }

        private async Task<Form> GetFormForSummary(FormSummary summary)
        {
            if (summary == null) return null;
            return await Store.GetFormAsync(summary.FormID);
        }

        public ICommand RefreshFormsClickHandler
        {
            get
            {
                return new CommandHandler(async () =>
                {
                    await Refresh();
                });
            }
        }

        public ICommand AddFormClickHandler
        {
            get
            {
                return new CommandHandler(() =>
                {
                });
            }
        }
    }
}
