#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using Newtonsoft.Json.Linq;

namespace EkoStatRp;

internal static class Constants
{
    public static SessionConstants Session { get; private set; }

    public static void Initialize(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        JObject data = JObject.Parse(json);
        Session = new SessionConstants((JObject)data[nameof(SessionConstants)]);
    }

    public class SessionConstants
    {
        public readonly string UserId;
        public readonly string UserName;

        public SessionConstants(JObject data)
        {
            UserId = data[nameof(UserId)].ToString();
            UserName = data[nameof(UserName)].ToString();
        }
    }
}

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
