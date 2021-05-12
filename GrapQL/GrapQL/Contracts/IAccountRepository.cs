using GrapQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.Contracts
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId);
        Task<ILookup<Guid, Account>> GetAccountsByOwnerIds(IEnumerable<Guid> ownerIds);
        Account GetById(Guid id);
        IEnumerable<Account> GetAll(int pageIndex, int pageSize);
        Account CreateAccount(Account account);
        Account UpdateAccount(Account dbAccount, Account account);
        void DeleteAccount(Account account);
        TotalRow GetTotalRow();
    }
}
