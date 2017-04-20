using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNETCF.FormManager
{
    public class FormService
    {
        private IFormStore m_store;

        private IFormStore Store
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

        public async Task<IEnumerable<FormSummary>> GetFormSummariesAsync()
        {
            return await Store.GetFormSummariesAsync();
        }

        public async Task<Form> GetFormAsync(int formID)
        {
            return await Store.GetFormAsync(formID);
        }

        public async Task StoreFormAsync(Form form)
        {
            await Store.StoreFormAsync(form);
        }
    }
}
