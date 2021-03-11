using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizManagerApp.Data
{
    public interface IQuizService
    {
        Task<IEnumerable<Quiz>> GetQuizTask();
        Task<QuizModel> GetQuizDetails(int QuizId);
        Task<QuizModel> FindQuestion(int QuestionId);
        Task<bool> CreateQuiz(Quiz quiz);
        Task<IEnumerable<QuizModel>> GetQuestionsForSingleQuiz(int id);
        Task<bool> CreateQuestionForSelectedQuiz(Question question);
        Task<IEnumerable<Answer>> GetAnswersForSingleQuestion(int QuestionId);
        Task<bool> DeleteQuiz(int QuizId);
    }
}