namespace DeltaQuestion.Data;

public class Comment
{
    public long Id { get; set; }
    public string Text { get; set; }
    public long QuestionId { get; set; }
    public long AuthorId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DownVote { get; set; }
    public int UpVote { get; set; }
    public int score => UpVote - DownVote>0?UpVote - DownVote:-(DownVote - UpVote);
    
    
}