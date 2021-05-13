using AutoMapper;
using Liquid.Core.Context;
using Liquid.Core.Telemetry;
using Liquid.Domain;
using MediatR;
using AvaBank.Domain.Model;
using AvaBank.Domain.Queries;
using AvaBank.Domain.Service;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AvaBank.Domain.Handler
{
    public class ListAccountsCommandHandler : RequestHandlerBase, IRequestHandler<ListAccountsQuery, IEnumerable<Account>>
    {
        private readonly IAccountServiceClient _accountService;

        public ListAccountsCommandHandler(IMediator mediatorService,
                                        ILightContext contextService,
                                        ILightTelemetry telemetryService,
                                        IMapper mapperService,
                                        IAccountServiceClient accountService)
            : base(mediatorService,
                  contextService,
                  telemetryService,
                  mapperService)
        {
            _accountService = accountService;
        }

        public async Task<IEnumerable<Account>> Handle(ListAccountsQuery request, CancellationToken cancellationToken)
        {
            return  await _accountService.SearchAccounts();
        }
    }
}