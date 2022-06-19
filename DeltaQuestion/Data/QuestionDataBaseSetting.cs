namespace DeltaQuestion.Data;

public class QuestionDataBaseSetting
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string QuestionCollectionName { get; set; } = null!;
}