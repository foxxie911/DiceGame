using System.Security.Cryptography;

namespace Foxxie911.DiceGame
{
    public class FairMoveGenerator
    {
        public static Dictionary<string, string> FirstMoveSelector()
        {
            int selection = RandomNumberGenerator.GetInt32(2);
            var validity = new ValidityService(selection);
            return new Dictionary<string, string>
            {
                {"Message", validity.Message},
                {"Key", validity.Key},
                {"HMAC", validity.HMAC}
            };
        }

        public static Dictionary<string, string> ThrowInputSelector()
        {
            int selection = RandomNumberGenerator.GetInt32(5);
            var validity = new ValidityService(selection);
            return new Dictionary<string, string>
            {
                {"Message", validity.Message},
                {"Key", validity.Key},
                {"HMAC", validity.HMAC}
            };
        }
    }
}