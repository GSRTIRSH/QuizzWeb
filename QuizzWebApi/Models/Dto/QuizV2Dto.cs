namespace QuizzWebApi.Models.Dto;

public class QuizV2Dto
{
    public string Title { get; set; }

    public List<QuestionV2> Questions { get; set; }

    public string Category { get; set; }

    public string Difficulty { get; set; }

    public string Author { get; set; }
}

public class AnswersDto
{
    public string Text { get; set; }

    public bool IsCorrect { get; set; }
}