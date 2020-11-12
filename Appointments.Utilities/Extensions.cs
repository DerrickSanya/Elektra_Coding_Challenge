namespace Appointments.Utilities
{
    using System;

    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// GetUniqueReferenceCode
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueReferenceCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            var stringChars = new char[6];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            return new string(stringChars);
        }
    }
}
