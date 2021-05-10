using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL.GraphQLType
{
    public class AccountInputType : InputObjectGraphType
    {
        public AccountInputType()
        {
            Name = "accountInput";
            Field<NonNullGraphType<AccountTypeEnumType>>("type");
            Field<NonNullGraphType<StringGraphType>>("description");
            Field<NonNullGraphType<GuidGraphType>>("ownerId");
        }
        
    }
}
