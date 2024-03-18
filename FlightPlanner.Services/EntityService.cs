using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class EntityService<T> : DbService, IEntityService<T> where T : Entity
    {
        public EntityService(IFlightPlannerDbContext context):base(context)
        {
                
        }
        public void Create(T entity)
        {
            lock(SharedLock.LockObject)
            {
                Create<T>(entity); 
            }          
        }

        public void Delete(T entity)
        {
            Delete<T>(entity);
        }

        public void DeleteAll()
        {
            DeleteAll<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return GetAll<T>();
        }

        public T? GetById(int id)
        {
            return GetById<T>(id);
        }

        public void Update(T entity)
        {
            Update<T>(entity);
        }

        
    }
}

