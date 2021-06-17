using Morpheus.Core.Filters;
using Morpheus.Core.Models;
using Morpheus.Core.Repositories;
using Morpheus.Infrastructure.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Morpheus.Data.Repositories
{
    public class PersonRespository : Repository, IPersonRespository
    {
        public PersonRespository(IDbConnector dbConnector) : base(dbConnector)
        {

        }

        public Task CreateAsync(PersonModel person)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<PersonModel> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PersonModel>> ListAsync(PersonFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PersonModel person)
        {
            throw new NotImplementedException();
        }
    }
}
