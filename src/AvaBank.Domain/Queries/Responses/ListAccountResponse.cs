using MediatR;
using AvaBank.Domain.Model;
using System.Collections.Generic;

namespace AvaBank.Domain.Queries
{
    public class ListMoviesResponse : IRequest
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}