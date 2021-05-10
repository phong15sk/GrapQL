using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL.GraphQLType
{
    public class PaginationInputType : InputObjectGraphType
    {
        public PaginationInputType()
        {
            Name = "paginationInput";
            Field<NonNullGraphType<StringGraphType>>("pageIndex");
            Field<NonNullGraphType<StringGraphType>>("pageSize");
        }
    }
}
