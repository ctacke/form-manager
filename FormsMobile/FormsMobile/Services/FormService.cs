using OpenNETCF.FormManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormsMobile.Services
{
    public interface IFormService
    {
        FormSummary[] GetFormSummaries();
    }

    class FormService : IFormService
    {
        public FormSummary[] GetFormSummaries()
        {
            // TODO: pull from local database (which gets periodically refreshed from the server in the background)
            return new FormSummary[]
                {
                    new FormSummary()
                    {
                         FormName = "Form 1",
                          Description = "This is a form for stuff"
                    },
                    new FormSummary()
                    {
                         FormName = "Form 2",
                          Description = "This is a form for things"
                    }
                };
        }
    }
}
