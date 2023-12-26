using Microsoft.AspNetCore.Mvc;
using Moq;
using QuizzWebApi.Controllers.v2;
using QuizzWebApi.Services.Interfaces;

namespace tests.Systems.Controllers;

public class QuizControllerTests
{
    [Fact]
    public async Task GetQuiz_Should_ReturnsNotFound_WhenQuizIsNull()
    {
        // Arrange
        const int quizId = 1;
        var quizServiceMock = new Mock<IQuizService>();

        QuizV2 quiz = null;
        quizServiceMock.Setup(service => service.GetQuiz(quizId)).ReturnsAsync(quiz);

        var controller = new QuizzesController(null, quizServiceMock.Object);

        // Act
        var result = await controller.GetQuiz(quizId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetQuiz_Should_ReturnsQuiz_WhenQuizExists()
    {
        // Arrange
        const int quizId = 1;
        var quizServiceMock = new Mock<IQuizService>();
        var quiz = new QuizV2
        {
            Id = quizId
        };
        quizServiceMock.Setup(service => service.GetQuiz(quizId)).ReturnsAsync(quiz);

        var controller = new QuizzesController(null, quizServiceMock.Object);

        // Act
        var result = await controller.GetQuiz(quizId);

        // Assert
        Assert.Equal(quiz, result.Value);
    }
}