using AutoMapper;
using Liquid.Cache;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using Liquid.Messaging;
using MediatR;
using Microsoft.Extensions.Options;
using AvaBank.Domain.Config;
using AvaBank.Domain.Exceptions;
using AvaBank.Domain.Messages.Publishers;
using AvaBank.Domain.Model;
using AvaBank.Domain.Queries;
using AvaBank.Domain.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AvaBank.Domain.Handler
{
    public class GetAccountCommandHandler : RequestHandlerBase, IRequestHandler<GetAccountQuery, Account>
    {
        private readonly IAccountServiceClient _accountService;
        private readonly ILightCache _cache;
        private readonly CacheConfig _cacheConfig;
        private readonly ILightProducer<AccountAproval> _lightProducer;

        public GetAccountCommandHandler(IMediator mediatorService,
                                      ILightContext contextService,
                                      ILightTelemetry telemetryService,
                                      IMapper mapperService,
                                      IAccountServiceClient accountService,
                                      ILightCache cache,
                                      IOptions<CacheConfig> cacheConfig,
                                      ILightProducer<AccountAproval> lightProducer)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _accountService = accountService;
            _cache = cache;
            _cacheConfig = cacheConfig.Value;
            _lightProducer = lightProducer;
        }

        public async Task<Account> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var result = await _cache.RetrieveOrAddAsync<Account>(
               key: $"AccountId:{request.accountId}",
               action: () =>
               {
                   return _accountService.GetAccount(request.accountId).Result;
               },
               expirationDuration: System.TimeSpan.FromMinutes(_cacheConfig.CacheTTLInMinutes));

            if (result is null)
            {
                throw new AccountNotFoundException();
            }
            else
            {
                await _lightProducer.SendMessageAsync(
                    message: new AccountAproval { Id = result.ImdbId, AccountName = result.Title },
                    customHeaders: new Dictionary<string, object>());
            }

            return result;
        }
    }
}