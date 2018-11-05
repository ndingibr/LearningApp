using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LearningApp.Infrastructure
{
    public class SecurityRepository<T> : ISecurityRepository<T> where T : class
    {
        private readonly SecurityContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public SecurityRepository(SecurityContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return entities.AsQueryable();
        }

        public T Get(long id)
        {
            //return entities.SingleOrDefault(s => s.Id == id);
            return null;
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

    }
}
