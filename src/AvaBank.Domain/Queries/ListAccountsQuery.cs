using MediatR;
using System.Collections.Generic;
using AvaBank.Domain.Model;

namespace AvaBank.Domain.Queries
{
    public class ListAccountsQuery : IRequest<IEnumerable<Account>>
    {
        public string SearchString { get; set; }
    }
}