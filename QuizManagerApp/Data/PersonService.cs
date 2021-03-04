using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QuizManagerApp.Data
{
    public class PersonService:IPersonService
    {
        private readonly SqlConnectionConfiguration _configuration;
        public PersonService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Person>> GetPeople()
        {
            IEnumerable<Person> people;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    people = await conn.QueryAsync<Person>("spViewPermissionsForEachUser", CommandType.StoredProcedure);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }

            }
            return people;
        }
    }

    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetPeople();
    }
}
