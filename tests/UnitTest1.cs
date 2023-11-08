using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzWebApi.Controllers.v1;
using QuizzWebApi.Controllers.v2;

namespace tests;

public class UnitTest1
{
    [Fact]
    public async void Test1()
    {
        // Arrange

        var mock = new Mock<QuizContextV2>();
        var mock2 = new Mock<ILogger>();
        //mock.Setups

        var controller = new QuestionController(mock.Object, mock2.Object);

        // Action
        var result = await controller.GetQuestions();

        // Assert
        Assert.Empty(result);
    }
}