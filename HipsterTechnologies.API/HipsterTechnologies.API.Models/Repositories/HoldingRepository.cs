using AutoMapper;
using HipsterTechnologies.API.Models.Contexts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HipsterTechnologies.API.Models.Repositories
{
    public class HoldingRepository : IHoldingRepository
    {
        public HoldingRepository(IModelContextFactory factory)
        {
            _factory = factory;
        }

        public Holding Create(Holding holding)
        {
            var result = default(Holding);
            using (var db = _factory.CreateModelContext())
            {
                result = db.Holdings.Add(holding);
                db.SaveChanges();
            }
            return Mapper.Map<Holding, Holding>(result);
        }

        public void Delete(Holding holding)
        {
            using (var db = _factory.CreateModelContext())
            {
                db.Holdings.Attach(holding);
                db.Holdings.Remove(holding);
                db.SaveChanges();
            }
        }

        public IEnumerable<Holding> GetHoldings(string username)
        {
            IQueryable<Holding> results = null;
            using (var db = _factory.CreateModelContext())
            {
                results = db.Holdings.Where(holding => holding.FacebookHandle == username);
            }
            return results;
        }

        public IEnumerable<Holding> GetHoldings(string username, params Func<Holding, bool>[] filters)
        {
            IQueryable<Holding> results = null;
            using (var db = _factory.CreateModelContext())
            {
                // Thin out the results to only those holdings which are 
                // owned by the specified user.
                results = db.Holdings.Where(holding => holding.FacebookHandle == username).AsQueryable();

                // Apply each of the filters to the queries.
                foreach (var filter in filters)
                {
                    results = results.Where(holding => filter.Invoke(holding)).AsQueryable();
                }
            }
            return results.ToList();
        }

        public void Update(Holding holding)
        {
            using (var db = _factory.CreateModelContext())
            {
                db.Holdings.Attach(holding);
                db.SetModified(holding);
                db.SaveChanges();
            }
        }

        private IModelContextFactory _factory;
    }
}
