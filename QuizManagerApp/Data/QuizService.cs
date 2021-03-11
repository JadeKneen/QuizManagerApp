using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

namespace QuizManagerApp.Data
{
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

        public async Task<QuizModel> GetQuizDetails(int QuizId)
        {
            QuizModel _quizModel = new QuizModel();
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                _quizModel = conn.QueryFirst<QuizModel>("spDeleteQuizWithAnswers", new {QuizId}, commandType: CommandType.StoredProcedure);
                conn.Close();
            }

            return _quizModel;
        }

        public async Task<QuizModel> FindQuestion(int QuestionId)
        {
            QuizModel questionDescription = new QuizModel();

            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                questionDescription = conn.QueryFirst<QuizModel>(
                    "SELECT * FROM dbo.Questions WHERE QuestionId = @QuestionId", new {QuestionId}, commandType:CommandType.Text);
                conn.Close();
            }
            return questionDescription;
        }

        public async Task<bool> CreateQuiz(Quiz quiz)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                await conn.ExecuteAsync("spAddQuiz", new { quiz.QuizId, Description = quiz.Description }, commandType: CommandType.StoredProcedure);
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

        public async Task<bool> CreateQuestionForSelectedQuiz(Question question)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                await conn.ExecuteAsync("spAddQuestion", new { question.QuestionId, question.Description, question.QuizId }, commandType: CommandType.StoredProcedure);
                conn.Close();
            }
            return true;
        }

        public async Task<IEnumerable<Answer>> GetAnswersForSingleQuestion(int QuestionId)
        {
            IEnumerable<Answer> answers;

            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                answers = await conn.QueryAsync<Answer>("Select * from dbo.AnswersTable where QuestionId = @QuestionId", new { QuestionId }, commandType: CommandType.Text);
                conn.Close();
            }

            return answers;
        }

        public async Task<bool> DeleteQuiz(int QuizId)
        {
            using (var conn = new SqlConnection(_configuration.Value))
            {
                conn.Open();
                await conn.ExecuteAsync("DELETE from dbo.Quiz WHERE QuizId=@QuizId", new {QuizId},
                    commandType: CommandType.Text);
                conn.Close();
            }

            return true;
        }
    }
}
