using GraphQL.Types;
using GrapQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.GrapQL.GraphQLType
{
    public class TotalRowType : ObjectGraphType<TotalRow>
    {
        public TotalRowType()
        {
            Field(x => x.Total).Description("Total of row");
        }
    }
}
