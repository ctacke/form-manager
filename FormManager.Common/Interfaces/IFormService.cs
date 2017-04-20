using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNETCF.FormManager
{
    public interface IFormStore
    {
        Task<IEnumerable<FormSummary>> GetFormSummariesAsync();
        Task<Form> GetFormAsync(int formID);
        Task StoreFormAsync(Form form);
        Task DeleteFormAsync(Form form);
    }
}
