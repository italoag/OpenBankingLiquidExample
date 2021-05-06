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
            var response = await GetAsync<Account>(endpoint: $"?apikey=2f93d90d&i={id}");

            Account result = null;

            if (response.HttpResponse.IsSuccessStatusCode)
            {
                result = await response.GetContentObjectAsync();

                if (result.Response.ToUpper().Equals(bool.FalseString.ToUpper()))
                    return null;                
            }

            return result;
        }

        public async Task<IEnumerable<Account>> SearchAccounts(string query)
        {
            SearchResult result = null;
            var httpResponse = await GetAsync<SearchResult>($"?apikey=2f93d90d&s={query}");

            if (httpResponse.HttpResponse.IsSuccessStatusCode)
            {
                result = await httpResponse.GetContentObjectAsync();

                return result.Search;
            }

            return null;
        }
    }
}