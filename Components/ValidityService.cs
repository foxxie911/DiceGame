using System;
using System.Security.Cryptography;

namespace Foxxie911.DiceGame.Components
{
    public class ValidityService
    {
        readonly int message;
        readonly byte[] key;
        readonly byte[] hmac;

        public ValidityService(int message)
        {
            this.message = message;
            this.key = GenerateRandomKey();
            this.hmac = CalculateHMAC(key, message);
        }


        private static byte[] GenerateRandomKey()
        {
            byte[] key = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }
            return key;
        }

        private static byte[] CalculateHMAC(byte[] key, int message)
        {
            byte[] messageBytes = BitConverter.GetBytes(message);
            return HMACSHA3_256.HashData(key, messageBytes);
        }

        public string Key{
            get{
                return Convert.ToHexString(key);
            }
        }

        public string HMAC{
            get{
                return Convert.ToHexString(hmac);
            }
        }

        public string Message{
            get{
                return message.ToString();
            }
        }
    }
}