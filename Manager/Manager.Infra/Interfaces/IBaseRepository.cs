using Manager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Infra.Interfaces
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<T> Create(T obj);
        Task Delete(long id);
        Task<T> Update(T obj);
        Task<T> Get(long id);
        Task<List<T>> Get();
    }
}
