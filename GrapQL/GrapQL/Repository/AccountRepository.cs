using GrapQL.Contracts;
using GrapQL.Entities.Context;
using GrapQL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationContext _context;
        public AccountRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Account> GetAllAccountsPerOwner(Guid ownerId) => _context.Accounts
            .Where(a => a.OwnerId.Equals(ownerId))
            .ToList();
        public async Task<ILookup<Guid, Account>> GetAccountsByOwnerIds(IEnumerable<Guid> ownerIds)
        {
            var accounts = await _context.Accounts.Where(a => ownerIds.Contains(a.OwnerId)).ToListAsync();
            return accounts.ToLookup(x => x.OwnerId);
        }
        public Account GetById(Guid id) => _context.Accounts.SingleOrDefault(o => o.Id.Equals(id));
        public IEnumerable<Account> GetAll(int pageIndex, int pageSize)
        {
            return _context.Accounts.FromSqlInterpolated($"spGetAllAccount {pageIndex}, {pageSize}").ToList();
        }
        public Account CreateAccount(Account account)
        {
            account.Id = Guid.NewGuid();
            _context.Add(account);
            _context.SaveChanges();
            return account;
        }
        public Account UpdateAccount(Account dbAccount, Account account)
        {
            dbAccount.Description = account.Description;
            dbAccount.Type = account.Type;
            dbAccount.OwnerId = account.OwnerId;
            _context.SaveChanges();
            return dbAccount;
        }
        public void DeleteAccount(Account account)
        {
            _context.Remove(account);
            _context.SaveChanges();
        }
        public TotalRow GetTotalRow()
        {
            var totalRow = new TotalRow();
            totalRow.Total = _context.Accounts.Count();
            return totalRow;
        }
    }
}
