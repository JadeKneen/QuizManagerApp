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
        Task<bool> CreateQuiz(Quiz quiz);
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
                conn.Open();
                quizzes = await conn.QueryAsync<Quiz>("SELECT * FROM dbo.Quiz");
                conn.Close();
            }
            return quizzes;
        }

        public async Task<bool> CreateQuiz(Quiz quiz)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                await conn.ExecuteAsync("spAddQuiz", new { quiz.QuizId, quiz.Description }, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return true;
        }
    }

    public class Quiz
    {
        public int QuizId { get; set; }
        public string Description { get; set; }
    }
}
