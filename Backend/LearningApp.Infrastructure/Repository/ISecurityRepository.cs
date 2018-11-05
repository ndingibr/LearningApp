using System.Linq;

namespace LearningApp.Infrastructure
{
    public interface ISecurityRepository<T> 
    {
        IQueryable<T> GetAll();
        T Get(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}