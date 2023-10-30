namespace CMS.Models;

public class QuestionV2
{
    public int Id { get; set; }

    public string Title { get; set; }

    public Dictionary<char, string> Answers { get; set; }

    public Dictionary<char, string> CorrectAnswers { get; set; }

    public int QuizV2Id { get; set; }
}