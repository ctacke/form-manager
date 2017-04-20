using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenNETCF.FormManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FormBuilder.Services
{
    class RESTFormStore : IFormStore
    {
        public void DeleteForm(Form form)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteFormAsync(Form form)
        {
            throw new NotImplementedException();
        }

        public Form GetForm(int formID)
        {
            throw new NotImplementedException();
        }

        public Form GetForm(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Form> GetFormAsync(int formID)
        {
            try
            {
                var c = new HttpClient();

                var response = await c.GetAsync(string.Format("http://localhost:8080/api/forms/{0}", formID));
                var data = await response.Content.ReadAsStringAsync();
                var form = JsonConvert.DeserializeObject<Form>(data);
                return form;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Form> GetForms()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FormSummary>> GetFormSummariesAsync()
        {
            try
            {
                var c = new HttpClient();

                var response = await c.GetAsync("http://localhost:8080/api/forms");
                var data = await response.Content.ReadAsStringAsync();
                var forms = JsonConvert.DeserializeObject<FormSummary[]>(data);
                return forms;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void StoreForm(Form form)
        {
            throw new NotImplementedException();
        }

        public async Task StoreFormAsync(Form form)
        {
            throw new NotImplementedException();
        }
    }
}
