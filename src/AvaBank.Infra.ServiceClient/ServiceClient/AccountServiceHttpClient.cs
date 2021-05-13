using AutoMapper;
using Liquid.Core.Configuration;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Messaging;
using Liquid.Services.Configuration;
using Liquid.Services.Http;
using Microsoft.Extensions.Logging;
using AvaBank.Domain.Messages.Publishers;
using AvaBank.Domain.Model;
using AvaBank.Domain.Service;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AvaBank.Infra.ServiceClient
{
    public class AccountServiceHttpClient : LightHttpService, IAccountServiceClient
    {
        public AccountServiceHttpClient(IHttpClientFactory httpClientFactory,
                           ILoggerFactory loggerFactory,
                           ILightContextFactory contextFactory,
                           ILightTelemetryFactory telemetryFactory,
                           ILightConfiguration<List<LightServiceSetting>> servicesSettings,
                           IMapper mapperService)
            : base(httpClientFactory,
                  loggerFactory,
                  contextFactory,
                  telemetryFactory,
                  servicesSettings,
                  mapperService)
        {
            
        }

        public async Task<Account> GetAccount(string id)
        {
            Dictionary<string, string> headers = GetHeaders();

            var response = await GetAsync<Account>(endpoint: $"credit-cards-accounts/v1/accounts/{id}", headers);

            Account result = null;

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                result = await response.GetContentObjectAsync();
            }

            return result;
        }

        private static Dictionary<string, string> GetHeaders()
        {
            var headers = new Dictionary<string, string>();

            headers.Add("Ocp-Apim-Subscription-Key", "595c4615262c4ebf9e67e2f527613595");
            headers.Add("Accept", "application/json");
            headers.Add("Authorization", "0a7546ded89dea65");
            headers.Add("x-fapi-auth-date", "Sun, 10 Sep 2017 19:43:31 UTC");
            headers.Add("x-fapi-customer-ip-address", "127.0.0.1");
            headers.Add("x-fapi-interaction-id", "string");
            headers.Add("x-customer-user-agent", "string");
            return headers;
        }

        public async Task<IEnumerable<Account>> SearchAccounts()
        {
            Dictionary<string, string> headers = GetHeaders();
            SearchResult result = null;
            var httpResponse = await GetAsync<SearchResult>($"credit-cards-accounts/v1/accounts", headers);

            if (httpResponse.HttpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.GetContentObjectAsync();

                return result.Search;
            }

            return null;
        }
    }
}