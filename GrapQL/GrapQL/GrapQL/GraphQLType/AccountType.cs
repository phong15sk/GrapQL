using GraphQL.DataLoader;
using GraphQL.Types;
using GrapQL.Contracts;
using GrapQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL
{
    public class AccountType : ObjectGraphType<Account>
    {
        public AccountType(IOwnerRepository repository, IDataLoaderContextAccessor dataLoader)
        {
            Field(x => x.Id, type: typeof(IdGraphType)).Description("Id property from the account object.");
            Field(x => x.Description).Description("Description property from the account object.");
            Field(x => x.OwnerId, type: typeof(IdGraphType)).Description("OwnerId property from the account object.");
            Field<AccountTypeEnumType>("Type", "Enumeration for the account type object.");
        }
    }
}
