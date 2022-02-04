using System;

namespace CAAS.Utilities
{
    public static class SupportedOperations
    {
        public static class Symmetric
        {
            public static class aes
            {
                public static string encrypt()
                {
                    return "AES Encrypt";
                }
                public static string decrypt()
                {
                    return "AES Decrypt";
                }
            }
        }
    }
}
