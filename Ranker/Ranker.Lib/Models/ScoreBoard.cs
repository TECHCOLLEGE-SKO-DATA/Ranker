namespace Ranker.Lib.Models;

public class ScoreBoard : BaseModel, IModel 
{
    public int ScoreBoardId { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public string UniqueKey = String.Empty;
    public int SettingId { get; set; }

    public string Validate() 
    {
        if (ScoreBoardId <= 0)
        {
            return "Invalid League Id";
        }
        if (Name.Length < 3)
        {
            return "Name must be at least 3 characters long";
        }
        if (UniqueKey.Length != 8)
        {
            return "UniqueKey must be exactly 8 characters";
        }
        if (SettingId <= 0)
        {
            return "Invalid Settings Id";
        }

        return "";
    }

}
