using CMS.Models;

namespace CMS.Data;

public class QuizContext
{
    public QuizContext()
    {
        Quizzes = new List<Quiz>();
        var client = new HttpClient();

        const string baseUrl = "http://localhost:5200/api/quizzes";

        var quizzes = client.GetFromJsonAsync<List<Quiz>>(baseUrl).Result;

        if (quizzes != null)
        {
            Quizzes = quizzes;
        }
    }

    public List<Quiz>? Quizzes { get; set; }
}