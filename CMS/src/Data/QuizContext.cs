using CMS.Models;

namespace CMS.Data;


public class QuizContext
{
    public QuizContext()
    {
        Quizzes = new List<Quiz>();
        
        const string baseUrl = "http://localhost:5200/api";
        var clientApi = new ClientApi(baseUrl, new HttpClient());

        var quizzes = clientApi.GetAsync<List<Quiz>>("v2/quizzes").Result;

        if (quizzes != null)
        {
            Quizzes = quizzes;
        }
    }

    public List<Quiz>? Quizzes { get; set; }
}

public class QuestionContext
{
    public QuestionContext()
    {
        Questions = new List<QuestionV2>();

        const string baseUrl = "http://localhost:5200/api";
        var clientApi = new ClientApi(baseUrl, new HttpClient());

        var questions = clientApi.GetAsync<List<QuestionV2>>("v2/question").Result;

        if (questions != null)
        {
            Questions = questions;
        }
    }

    public List<QuestionV2>? Questions { get; set; }
}