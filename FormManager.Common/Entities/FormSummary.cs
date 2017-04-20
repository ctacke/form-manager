using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OpenNETCF.FormManager
{
    public class FormSummary : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_formName;
        private string m_description;

        public int FormID { get; set; }

        public string FormName
        {
            get { return m_formName; }
            set
            {
                m_formName = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return m_description; }
            set
            {
                m_description = value;
                RaisePropertyChanged();
            }
        }

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
