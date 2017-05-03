using OpenNETCF;
using OpenNETCF.FormManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormsMobile.Services
{
    class DataSyncService : DataSyncServiceBase
    {
        public string ServerAddress { get; private set; }

        public DataSyncService(string serverAddress)
        {
            Validate
                .Begin()
                .ParameterIsNotNull(serverAddress, "serverAddress")
                .Check();

            if (!serverAddress.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                serverAddress = "http://" + serverAddress;
            }

            if (serverAddress.EndsWith("/"))
            {
                ServerAddress = serverAddress;
            }
            else
            {
                ServerAddress = serverAddress + "/";
            }
        }

        protected async override Task OnPublishAsync()
        {
            // TODO: this simply eliminates the warning for now
            await Task.Delay(0);
        }

        protected async override Task OnReceiveAsync()
        {
            try
            {
                // get all summaries
                var summaries = await GetEntity<FormSummary[]>(ServerAddress + "api/forms");

                // get all forms
                foreach (var summary in summaries)
                {
                    try
                    {
                        // TODO: only get forms that have changed (add support for "last changed" field)
                        var path = string.Format("{0}api/forms{1}", ServerAddress, summary.FormID);
                        var form = await GetEntity<Form>(ServerAddress + "api/forms");

                        CreateOrUpdateLocalForm(form);
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void CreateOrUpdateLocalForm(Form serverSource)
        {
        }

    }
}
