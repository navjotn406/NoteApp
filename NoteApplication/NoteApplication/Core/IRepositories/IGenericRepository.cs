using System.Linq.Expressions;

namespace NoteApplication.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        List<T> GetAll();
        List<T> GetAllBy(Expression<Func<T, bool>> predicate);
        T GetBy(Expression<Func<T, bool>> predicate);
        void Create(T t);
        void Delete(int id);
        void Update(T t);
    }
}