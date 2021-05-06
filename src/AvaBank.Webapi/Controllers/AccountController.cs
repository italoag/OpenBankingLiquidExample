using Liquid.Core.Context;
using Liquid.Core.Localization;
using Liquid.Core.Telemetry;
using Liquid.WebApi.Http.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AvaBank.Domain.Queries;
using System.Threading.Tasks;

namespace AvaBank.Webapi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AccountsController : BaseController
    {
        public AccountsController(ILoggerFactory loggerFactory,
                                  IMediator mediator,
                                  ILightContext context,
                                  ILightTelemetry telemetry,
                                  ILocalization localization)
            : base(loggerFactory,
                  mediator,
                  context,
                  telemetry,
                  localization)
        { } 

        [HttpGet()]
        public async Task<IActionResult> SearchAccounts([FromQuery(Name="nameSearch")] string nameSearch) =>
            await ExecuteAsync(new ListAccountsQuery() { SearchString = nameSearch });

        [HttpGet(template: "{id}")]
        public async Task<IActionResult> GetAccount(string id) =>
            await ExecuteAsync(new GetAccountQuery() { ImdbId = id });
    }
}