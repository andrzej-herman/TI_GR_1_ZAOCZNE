using Quiz.Data.Dtos;

namespace Quiz.Application.Services
{
    public interface IGameService
    {
        Task<QuestionDto?> GetQuestion(int category);
        Task<CheckAnswer?> CheckAnswer(Guid answerId, int category);
    }
}
