using GraphQL;
using GraphQL.Types;
using GrapQL.Contracts;
using GrapQL.Model;
using GrapQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL.GraphQLQueries
{
    public class AppQuery : ObjectGraphType
    {
        private readonly IOwnerRepository _ownerrepository;
        private readonly IAccountRepository _accountRepository;
        public AppQuery(IOwnerRepository ownerrepository,
                        IAccountRepository accountRepository)
        {
            _ownerrepository = ownerrepository;
            _accountRepository = accountRepository;

            Field<ListGraphType<OwnerType>>(
               "owners",
               resolve: context => _ownerrepository.GetAll()
            );
            Field<OwnerType>(
            "owner",
            arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "ownerId" }),
            resolve: context =>
            {
                Guid id;
                if (!Guid.TryParse(context.GetArgument<string>("ownerId"), out id))
                {
                    context.Errors.Add(new ExecutionError("Wrong value for guid"));
                    return null;
                }
                return _ownerrepository.GetById(id);
            }
            );
            Field<ListGraphType<AccountType>>(
               "accounts",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pageIndex" },
                new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "pageSize" }),
               resolve: context =>
               {
                   int pageIndex = int.Parse(context.GetArgument<StringGraphType>("pageIndex").ToString());
                   int pageSize = int.Parse(context.GetArgument<StringGraphType>("pageSize").ToString());
                   return _accountRepository.GetAll(pageIndex, pageSize);
               }
            );
        }
    }
}
