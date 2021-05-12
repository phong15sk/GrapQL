using GrapQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrapQL.Contracts
{
    public interface IOwnerRepository
    {
        IEnumerable<Owner> GetAll(int pageIndex, int pageSize);
        Owner GetById(Guid id);
        Owner CreateOwner(Owner owner);
        Owner UpdateOwner(Owner dbOwner, Owner owner);
        void DeleteOwner(Owner owner);
        TotalRow GetTotalRow();
    }
}
