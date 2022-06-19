using DeltaQuestion.Data;

namespace DeltaQuestion.Models;

public class DisplayQuestion
{
    public Question Question { get; set; }
    public List<Comment> Comments { get; set; }
}