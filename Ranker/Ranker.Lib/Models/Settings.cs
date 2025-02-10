namespace Ranker.Lib.Models;

public class Setting : IModel
{
    public int SettingId { get; set; }
    public int MaxRounds { get; set; }

    public string Validate()
    {
        if (SettingId <= 0)
        {
            return "Invalid Settings Id";
        }
        if (MaxRounds <= 0)
        {
            return "Maximum number of rounds must at least be 1";
        }

        return "";
    }
}
