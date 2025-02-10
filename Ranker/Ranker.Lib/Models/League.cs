namespace Ranker.Lib.Models;

public class League : IModel
{
    public int LeagueId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UniqueKey;
    public int SettingId { get; set; }

    public string Validate() 
    {
        if (LeagueId <= 0)
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
