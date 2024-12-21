using Quiz.Data.Dtos;

namespace Quiz.Api.Services
{
    public interface IQuizService
    {
        Task<QuestionDto?> GetQuestionAsync(int category);
        Task<CheckAnswer> CheckAnswerAsync(Guid answerId, int category);
    }
}
