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
    public class EventRepository : IEventRepository
    {
        public EventRepository(IModelContextFactory factory)
        {
            _factory = factory;
        }

        public Event Create(Event theEvent)
        {
            var result = default(Event);
            using (var db = _factory.CreateModelContext())
            {
                result = db.Events.Add(theEvent);
                db.SaveChanges();
            }
            return Mapper.Map<Event, Event>(result);
        }

        public void Delete(Event theEvent)
        {
            using (var db = _factory.CreateModelContext())
            {
                db.Events.Attach(theEvent);
                db.Events.Remove(theEvent);
                db.SaveChanges();
            }
        }

        public IEnumerable<Event> GetEvents(string username)
        {
            IQueryable<Event> results = null;
            using (var db = _factory.CreateModelContext())
            {
                results = db.Events.Where(theEvent => theEvent.FacebookHandle == username);
            }
            return results;
        }

        public IEnumerable<Event> GetEvents(string username, params Func<Event, bool>[] filters)
        {
            IQueryable<Event> results = null;
            using (var db = _factory.CreateModelContext())
            {
                // Thin out the results to only those events which are 
                // owned by the specified user.
                results = db.Events.Where(theEvent => theEvent.FacebookHandle == username).AsQueryable();

                // Apply each of the filters to the queries.
                foreach (var filter in filters)
                {
                    results = results.Where(theEvent => filter.Invoke(theEvent)).AsQueryable();
                }
            }
            return results.ToList();
        }

        public void Update(Event theEvent)
        {
            using (var db = _factory.CreateModelContext())
            {
                db.Events.Attach(theEvent);
                db.SetModified(theEvent);
                db.SaveChanges();
            }
        }

        private IModelContextFactory _factory;
    }
}
