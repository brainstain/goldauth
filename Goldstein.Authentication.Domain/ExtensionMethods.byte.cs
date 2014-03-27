using System.Linq;
using System.Security.Cryptography;

namespace Goldstein.Authentication.Domain
{
    public static partial class ExtensionMethods
    {
        public static byte[] Hash(this byte[] value, byte[] salt)
        {
            var hashedValue = value.Concat(salt).ToArray();
            return new SHA256Managed().ComputeHash(hashedValue);
        }
    }
}
