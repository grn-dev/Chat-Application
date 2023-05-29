using System;
using System.Threading.Tasks;
using RestSharp;

namespace Chat.Infra.Clients.HttpClient
{
    public class HttpRestClient
    {
        protected readonly IRestClient Client;
        protected readonly IServiceProvider ServiceProvider;

        public HttpRestClient(IRestClient client, IServiceProvider serviceProvider)
        {
            Client = client;
            ServiceProvider = serviceProvider;
        }

        public virtual Task<IRestResponse> SendRequestAsync(IRestRequest request)
        {
            return
                Client.ExecuteAsync(request);
        }
    }
}
