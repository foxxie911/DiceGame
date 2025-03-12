// TODO Implement Fair Number Generation of the dice selection, throw selection etc.
// Fair is when it will use both computer and user input to generate the random selection.

using System.Security.Cryptography;

namespace Foxxie911.DiceGame.Components
{
    public class FairMoveGenerator
    {
        public static Dictionary<string,string> FirstMoveSelector()
        {
            int selection = RandomNumberGenerator.GetInt32(1);
            var validity = new ValidityService(selection);
            return new Dictionary<string,string>
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