using Dapper;
using Dapper.Contrib.Extensions;
using Morpheus.Core.Filters;
using Morpheus.Core.Models;
using Morpheus.Core.Repositories;
using Morpheus.Infrastructure.Infrastructure.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Morpheus.Data.Repositories
{
    public class PersonRespository : Repository, IPersonRespository
    {
        public PersonRespository(IDbConnector dbConnector) : base(dbConnector) { }

        const string sqlBase = @"SELECT p.[Id]
                                      ,p.[Name]
                                      ,p.[Email]
                                      ,p.[CreatedAt]
                                 FROM [dbo].[Person] p 
                                 WHERE 1 = 1 ";


        public async Task CreateAsync(PersonModel person)
        {
            await base.Connection.InsertAsync(person, transaction: base.Transaction);
        }

        public async Task UpdateAsync(PersonModel person)
        {
            await base.Connection.UpdateAsync(person, transaction: base.Transaction);
        }

        public async Task<PersonModel> GetByIdAsync(string id)
        {
            string query = $"{sqlBase} AND p.[Id] = @Id ";

            var pessoa = await base.Connection.QueryAsync<PersonModel>(query, new { Id = id }, transaction: base.Transaction);

            return pessoa.FirstOrDefault();
        }

        public async Task<IEnumerable<PersonModel>> ListAsync(PersonFilter filter)
        {
            var query = $"{sqlBase} @DynamicFilter";

            var paramenters = new DynamicParameters();

            ApplyFilter(query, filter, paramenters);

            var pessoas = await base.Connection.QueryAsync<PersonModel>(query, paramenters, transaction: base.Transaction);

            return pessoas;
        }

        public async Task<bool> ExistsByIdAsync(string id)
        {
            string query = $"SELECT TOP 1 1 FROM [dbo].[Person] WHERE [Id] = @Id ";

            var pessoa = await base.Connection.QueryAsync<bool>(query, new { Id = id }, transaction: base.Transaction);

            return pessoa.FirstOrDefault();
        }

        public async Task DeleteAsync(PersonModel person)
        {
            await base.Connection.DeleteAsync(person, transaction: base.Transaction);
        }

        private void ApplyFilter(string sql, PersonFilter filter, DynamicParameters paramenters)
        {
            var conditions = new Collection<string>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                conditions.Add("p.[Email] LIKE @Email");
                paramenters.Add("Email", "%" + filter.Email + "%");
            }

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                conditions.Add("p.[Name] LIKE @Name");
                paramenters.Add("Name", "%" + filter.Name + "%");
            }

            var dynamicFilter = conditions.Any() ? $" AND {string.Join(" AND ", conditions)}" : "";

            sql.Replace("@DynamicFilter", dynamicFilter);
        }
    }
}