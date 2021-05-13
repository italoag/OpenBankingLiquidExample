using AvaBank.Domain.Model;
using System.Collections.Generic;

namespace AvaBank.Infra.ServiceClient
{
    internal class SearchResult
    {
        public IEnumerable<Account> Search { get; set; }
        public int TotalResults { get; set; }
        public bool Result { get; set; }
    }
}