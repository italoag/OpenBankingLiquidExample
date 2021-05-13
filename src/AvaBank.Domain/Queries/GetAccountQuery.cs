using MediatR;
using AvaBank.Domain.Model;

namespace AvaBank.Domain.Queries
{
    public class GetAccountQuery : IRequest<Account>
    {
        public string AccountId { get; set; }
    }
}