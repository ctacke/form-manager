using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OpenNETCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FormsMobile.Services
{
    // Required libraries:
    //   OpenNETCF.Extensions
    //   Newtonsoft Json.NET

    public abstract class DataSyncServiceBase : DisposableBase
    {
        protected abstract Task OnPublishAsync();
        protected abstract Task OnReceiveAsync();

        private int m_refreshPeriod = 60;
        private AsyncAutoResetEvent m_txDataReadyEvent = new AsyncAutoResetEvent();

        protected int MinRefreshPeriodSeconds { get; set; } = 15;       // 15 seconds
        protected int MaxRefreshPeriodSeconds { get; set; } = 15 * 60;  // 15 minutes
        private int m_startupWaitSeconds;

        public DataSyncServiceBase(int startupWaitSeconds = 5)
        {
            Validate
                .Begin()
                .ArgumentIsGreaterThan(startupWaitSeconds, 0, nameof(startupWaitSeconds))
                .Check();

            m_startupWaitSeconds = startupWaitSeconds;

            Task.Run(() => { ReceiveProc(); });
            Task.Run(() => { PublishProc(); });
        }

        public int DataRefreshPeriodSeconds
        {
            get { return m_refreshPeriod; }
            set
            {
                // bounds check - just ignore things that are nonsense
                if (value <= 0) return;
                if (value < MinRefreshPeriodSeconds) return;
                if (value > (MaxRefreshPeriodSeconds)) return;

                m_refreshPeriod = value;
            }
        }

        protected void DataReadyToSend()
        {
            m_txDataReadyEvent.Set();
        }

        private async void PublishProc()
        {
            while (!IsDisposed)
            {
                // TODO: publish any data to be sent
                await OnPublishAsync();

                // wait for the ready event (or 5 seconds)
                Task.WaitAny(
                    Task.Delay(5000),
                    m_txDataReadyEvent.WaitAsync()
                    );
            }
        }

        private async void ReceiveProc()
        {
            await Task.Delay(m_startupWaitSeconds);

            while (!IsDisposed)
            {
                await OnReceiveAsync();

                // now pause
                await Task.Delay(DataRefreshPeriodSeconds * 1000);
            }
        }

        private HttpClient m_client;

        protected virtual string GetRequestAuthorizationKey() { return null; }
        protected virtual string GetRequestAcceptHeader() { return null; }

        protected HttpClient ServiceClient
        {
            get
            {
                if (m_client == null)
                {
                    m_client = new HttpClient();
                    var authKey = GetRequestAuthorizationKey();
                    if (authKey != null)
                    {
                        m_client.DefaultRequestHeaders.Add("Authorization", authKey);
                    }
                    var accept = GetRequestAcceptHeader();

                    m_client.DefaultRequestHeaders.Add("Accept", accept ?? "application/json");
                }

                return m_client;
            }
        }

        protected async Task<T> GetEntity<T>(string path)
        {
            try
            {
                var data = await ServiceClient.GetStringAsync(path);
                var result = JsonConvert.DeserializeObject<T>(data, new IsoDateTimeConverter());
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
