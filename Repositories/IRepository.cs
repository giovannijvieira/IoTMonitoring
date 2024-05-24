using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTMonitoring.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string identifier);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string identifier);
    }
}
