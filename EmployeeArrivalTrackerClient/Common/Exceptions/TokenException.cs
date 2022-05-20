using System;

namespace Common.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException(string masege)
         : base(String.Format(masege))
        {
        }
    }
}
