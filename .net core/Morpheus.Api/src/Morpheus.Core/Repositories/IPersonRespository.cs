using Morpheus.Core.Filters;
using Morpheus.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpheus.Core.Repositories
{
    public interface IPersonRespository
    {
        Task CreateAsync(PersonModel person);
        Task UpdateAsync(PersonModel person);
        Task<PersonModel> GetByIdAsync(string id);
        Task<bool> ExistsByIdAsync(string id);
        Task<IEnumerable<PersonModel>> ListAsync(PersonFilter filter);
        Task DeleteAsync(PersonModel person);
    }
}
