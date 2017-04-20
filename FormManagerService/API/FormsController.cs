using OpenNETCF.FormManager;
using OpenNETCF.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace FormServer.Controllers
{
    [RoutePrefix("api")]
    public class FormsController : ApiController
    {
        private FormService m_formService;

        public FormsController()
        {
            m_formService = RootWorkItem.Services.GetOrAdd<FormService>();
        }

        [Route]
        [HttpGet]
        public HttpResponseMessage GetServiceInfo()
        {
            var payload = new
            {
                Name = "OpenNETCF Form Manager Service",
                Version = "0.0"
            };

            return Request.CreateResponse(HttpStatusCode.OK, payload, "application/json");
        }

        [Route("forms")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetFormList()
        {
            var forms = await m_formService.GetFormSummariesAsync();

            return Request.CreateResponse(HttpStatusCode.OK, forms, "application/json");
        }

        [Route("forms/{formID}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetForm(int formID)
        {
            var form =await  m_formService.GetFormAsync(formID);

            return Request.CreateResponse(HttpStatusCode.OK, form, "application/json");
        }
    }
}
