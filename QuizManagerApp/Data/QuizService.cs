using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QuizManagerApp.Data
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizTask();
        Task<bool> CreateQuiz(Quiz quiz);
        Task<IEnumerable<QuizModel>> GetQuestionsForSingleQuiz(int id);
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

        public async Task<IEnumerable<QuizModel>> GetQuestionsForSingleQuiz(int QuizId)
        {
            IEnumerable<QuizModel> quizModel;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                quizModel = await conn.QueryAsync<QuizModel>("Select * from dbo.Questions where QuizId = @QuizId", new {QuizId}, commandType:CommandType.Text);
                conn.Close();
            }

            return quizModel;
        }
    }

    public class QuizModel : Quiz
    {
        public int QuizId { get; set; }
        public string QuestionDescription { get; set; }
        public string QuizDescription { get; set; }
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public string Description { get; set; }
        public int QuizId { get; set; }
    }

    public class Quiz
    {
        public int QuizId { get; set; }
        public string Description { get; set; }
    }
}
