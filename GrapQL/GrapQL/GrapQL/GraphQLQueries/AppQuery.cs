using GraphQL;
using GraphQL.Types;
using GrapQL.Contracts;
using GrapQL.GrapQL.GraphQLType;
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
               arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<PaginationInputType>> { Name = "Pagination" }
                ),
               resolve: context =>
               {
                   var page = context.GetArgument<Pagination>("Pagination");
                   return _ownerrepository.GetAll(page.PageIndex, page.PageSize);
               }
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
            Field<TotalRowType>(
               "getTotalRowOwner",
               resolve: context =>
               {
                   return _ownerrepository.GetTotalRow();
               }
            );
            Field<TotalRowType>(
             "getTotalRowAcount",
             resolve: context =>
             {
                 return _accountRepository.GetTotalRow();
             }
          );
            Field<ListGraphType<AccountType>>(
               "accounts",
                arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<PaginationInputType>> { Name = "Pagination" }
                ),
               resolve: context =>
               {
                   var page = context.GetArgument<Pagination>("Pagination");
                   return _accountRepository.GetAll(page.PageIndex, page.PageSize);
               }
            );
            Field<AccountType>(
              "account",
           arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "accountId" }),
           resolve: context =>
           {
               Guid id;
               if (!Guid.TryParse(context.GetArgument<string>("accountId"), out id))
               {
                   context.Errors.Add(new ExecutionError("Wrong value for guid"));
                   return null;
               }
               return _accountRepository.GetById(id);
           }
           );

        }
    }
}
