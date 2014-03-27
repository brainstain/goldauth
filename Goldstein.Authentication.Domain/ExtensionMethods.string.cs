using System.Text;

namespace Goldstein.Authentication.Domain
{
    public static partial class ExtensionMethods
    {
        public static byte[] Hash(this string value, byte[] salt)
        {
            return Hash(Encoding.UTF8.GetBytes(value), salt);
        }
    }
}
