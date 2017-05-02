using OpenNETCF;
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

            ServerAddress = serverAddress;
        }

        protected async override Task OnPublishAsync()
        {
            // TODO: this simply eliminates the warning for now
            await Task.Delay(0);
        }

        protected async override Task OnReceiveAsync()
        {
            // TODO: this simply eliminates the warning for now
            await Task.Delay(0);

            // get all summaries

            // get all forms
        }
    }
}
