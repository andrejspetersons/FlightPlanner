using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Services
{
    public class DbService : IDbService
    {
        protected readonly IFlightPlannerDbContext _context;

        public DbService(IFlightPlannerDbContext context)
        {
            _context = context;
        }
        public void Create<T>(T entity) where T : Entity
        {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();         
        }

        public void Delete<T>(T entity) where T : Entity
        {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();   
        }

        public void DeleteAll<T>() where T : Entity
        {
            var entities = _context.Set<T>().ToList();
            _context.Set<T>().RemoveRange(entities);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
                return _context.Set<T>().ToList();          
        }

        public T? GetById<T>(int id) where T : Entity
        {
            return _context.Set<T>().SingleOrDefault(entity => entity.Id == id);
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
