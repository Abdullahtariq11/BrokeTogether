using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrokeTogether.Core.Utilities
{
    public static class Helpers<T>
    {
        public static bool ValidateObject(T? obj)
        {
            return obj != null;
        }
    }
}