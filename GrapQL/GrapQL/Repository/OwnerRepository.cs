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
    public class OwnerRepository : IOwnerRepository
    {
        private readonly ApplicationContext _context;
        public OwnerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public IEnumerable<Owner> GetAll(int pageIndex, int pageSize)
        {
            return _context.Owners.FromSqlInterpolated($"spGetAllOwner {pageIndex}, {pageSize}").ToList();
        }
        public Owner GetById(Guid id) => _context.Owners.SingleOrDefault(o => o.Id.Equals(id));
        public Owner CreateOwner(Owner owner)
        {
            owner.Id = Guid.NewGuid();
            _context.Add(owner);
            _context.SaveChanges();
            return owner;
        }
        public Owner UpdateOwner(Owner dbOwner, Owner owner)
        {
            dbOwner.Name = owner.Name;
            dbOwner.Address = owner.Address;
            _context.SaveChanges();
            return dbOwner;
        }
        public void DeleteOwner(Owner owner)
        {
            _context.Remove(owner);
            _context.SaveChanges();
        }
        public TotalRow GetTotalRow()
        {
            var totalRow = new TotalRow();
            totalRow.Total = _context.Owners.Count();
            return totalRow;
        }
    }
}

