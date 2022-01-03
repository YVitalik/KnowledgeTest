using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICrud<TModel> where TModel : class
    {
        Task<IEnumerable<TModel>> GetAllAsync();

        Task<TModel> GetByIdAsync(int id);

        Task AddAsync(TModel model);

        Task UpdateAsync(TModel model);

        Task DeleteByIdAsync(int modelId);

    }
}
