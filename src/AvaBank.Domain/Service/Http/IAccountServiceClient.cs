using AvaBank.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AvaBank.Domain.Service
{
    public interface IAccountServiceClient
    {
        public Task<IEnumerable<Account>> SearchAccounts();

        public Task<Account> GetAccount(string id);
    }
}