using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Utilities
{
    public static class InviteCodeGenerator
    {
        private static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();

        public static string GenerateCode(int length = 6)
        {
            var data = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data);

            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = _chars[data[i] % _chars.Length];
            }
            return new string(result);
        }
    }
}