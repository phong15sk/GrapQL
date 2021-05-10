using GraphQL;
using GraphQL.Types;
using GrapQL.Contracts;
using GrapQL.GrapQL.GraphQLType;
using GrapQL.Model;
using GrapQL.Pages;
using GrapQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL.GraphQLQueries
{
    public class AppMutation : ObjectGraphType
    {
        private readonly IOwnerRepository _ownerrepository;
        private readonly IAccountRepository _accountRepository;
        public AppMutation(IOwnerRepository ownerrepository,
                           IAccountRepository accountRepository)
        {
            _ownerrepository = ownerrepository;
            _accountRepository = accountRepository;

            Field<OwnerType>(
                "createOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" }),
                resolve: context =>
                {
                    var owner = context.GetArgument<Owner>("owner");
                    return _ownerrepository.CreateOwner(owner);
                }
            );
            Field<OwnerType>(
            "updateOwner",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<OwnerInputType>> { Name = "owner" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
                resolve: context =>
                {
                    var owner = context.GetArgument<Owner>("owner");
                    var ownerId = context.GetArgument<Guid>("ownerId");
                    var dbOwner = _ownerrepository.GetById(ownerId);
                    if (dbOwner == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
                        return null;
                    }
                    return _ownerrepository.UpdateOwner(dbOwner, owner);
                }
          );
            Field<StringGraphType>(
            "deleteOwner",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
                resolve: context =>
                {
                  var ownerId = context.GetArgument<Guid>("ownerId");
                  var owner = _ownerrepository.GetById(ownerId);
                  if (owner == null)
                  {
                      context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
                      return null;
                  }
                    _ownerrepository.DeleteOwner(owner);
                  return $"The owner with the id: {ownerId} has been successfully deleted from db.";
                }
          );
            Field<AccountType>(
                 "createAccount",
                 arguments: new QueryArguments(new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" }),
                 resolve: context =>
                 {
                     var account = context.GetArgument<Account>("account");
                     return _accountRepository.CreateAccount(account);
                 }
             );
            Field<AccountType>(
                 "updateAccount",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<AccountInputType>> { Name = "account" },
                new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" }),
                resolve: context =>
                {
                    var account = context.GetArgument<Account>("account");
                    var accountId = context.GetArgument<Guid>("accountId");
                    var dbAccount = _accountRepository.GetById(accountId);
                    if (dbAccount == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find account in db."));
                        return null;
                    }
                    return _accountRepository.UpdateAccount(dbAccount, account);
                }
          );
            Field<StringGraphType>(
            "deleteAccount",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" }),
                resolve: context =>
                {
                    var accountId = context.GetArgument<Guid>("accountId");
                    var account = _accountRepository.GetById(accountId);
                    if (account == null)
                    {
                        context.Errors.Add(new ExecutionError("Couldn't find owner in db."));
                        return null;
                    }
                    _accountRepository.DeleteAccount(account);
                    return $"The account with the id: {accountId} has been successfully deleted from db.";
                }
          );
        }

    }
}
