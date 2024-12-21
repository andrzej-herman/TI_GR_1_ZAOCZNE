using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Quiz.Data.Dtos;
using Quiz.Data.Entities;

namespace Quiz.Api.Services
{
    public class QuizService : IQuizService
    {
        Random _random;
        SqlConnection _connection;
        private const string connectionString = "Server=tcp:projektysan.database.windows.net,1433;Initial Catalog=sanquiz;Persist Security Info=False;User ID=aherman;Password=yxFH#D8w1SabJ1TAH99f;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public QuizService()
        {
            _connection = new SqlConnection(connectionString);
            _random = new Random();
        }


        public async Task<QuestionDto?> GetQuestionAsync(int category)
        {
            var questions = new List<QuestionDto>();
            await _connection.OpenAsync();
            var query = $"select * from Questions where QuestionCategory = {category}";
            var command = new SqlCommand(query, _connection);
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                var questionId = reader.GetGuid(0);
                var questionCategory = reader.GetInt32(1);
                var questionContent = reader.GetString(2);
                questions.Add(new QuestionDto
                {
                    Id = questionId,
                    Category = questionCategory,
                    Content = questionContent,
                    Answers = []
                }
                );
            }

             await reader.CloseAsync();
            if (!questions.Any()) return null;

            var index = _random.Next(0, questions.Count);
            var selectedQuestion = questions[index];
            var queryAnswers = $"select * from Answers where QuestionId = '{selectedQuestion.Id}'";
            var commandAnswers = new SqlCommand(queryAnswers, _connection);
            var readerAnswers = await commandAnswers.ExecuteReaderAsync();
            while (readerAnswers.Read())
            {
                var answerId = readerAnswers.GetGuid(0);
                var answerContent = readerAnswers.GetString(1);
                selectedQuestion.Answers.Add(new AnswerDto { Id = answerId, Content = answerContent });
            }

            await _connection.CloseAsync();
            return selectedQuestion;
        }


        public async Task<CheckAnswer> CheckAnswerAsync(Guid answerId, int category)
        {
            bool IsCorrect = false;
            List<int> categories = [100, 200, 300, 400, 500, 750, 1000];
            await _connection.OpenAsync();
            var query = $"select AnswerIsCorrect from Answers where AnswerId = '{answerId}'";
            var command = new SqlCommand(query, _connection);
            var reader = await command.ExecuteReaderAsync();
            while (reader.Read())
            {
                IsCorrect = reader.GetBoolean(0);
            }


            await _connection.CloseAsync();
            var currentIndex = categories.IndexOf(category);
            var nextCategory =  currentIndex != 6 ? categories[currentIndex + 1] : 0;
            return new CheckAnswer { IsCorrect = IsCorrect, NextCategory = nextCategory };
        }
    }
}
