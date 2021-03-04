using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QuizManagerApp.Data
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizTask();
    }

    public class QuizService : IQuizService
    {
        private readonly SqlConnectionConfiguration _configuration;

        public QuizService(SqlConnectionConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Quiz>> GetQuizTask()
        {
            IEnumerable<Quiz> quizzes;
            using (var conn = new SqlConnection(_configuration.Value))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                try
                {
                    quizzes = await conn.QueryAsync<Quiz>("SELECT * FROM dbo.Quiz");

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
            return quizzes;
        }
    }

    public class Quiz
    {
        public int QuizId { get; set; }
        public string Description { get; set; }
    }
}
