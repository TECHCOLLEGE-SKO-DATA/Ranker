using Ranker.Lib.Models;
using System.Text;

namespace Ranker.Lib;

public class Utility
{
    public static string CreateScoreboardUniqueKey()
    {
        Random rnd = new Random();
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        while (true)
        {
            int length = 8;
            StringBuilder res = new StringBuilder();

            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }

            string key = res.ToString();
            return key;
        }
    }
}