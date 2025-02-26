namespace Ranker.Lib.Models;

public class Score : BaseModel, IModel
{
    public int ScoreId { get; set; }
    public int ScoreBoardId { get; set; }
    public string ParticipantName { get; set; } = string.Empty;
    public int Points { get; set; }

    public string Validate()
    {
        if (ScoreId <= 0)
        {
            return "Invalid Score Id";
        }
        if (ScoreBoardId <= 0)
        {
            return "Invalid League Id";
        }
        if (ParticipantName.Length < 2)
        {
            return "Name of participant must be at least 2 characters long";
        }
        if (Points < 0)
        {
            return "Points can not go below 0";
        }

        return "";
    }
}