using System;

namespace App.Util
{
    public class IllegalStateException : Exception
    {
        public IllegalStateException(string message) : base(message)
        {
        }

        public IllegalStateException(string message, Exception inner) : base(message, inner)
        {
        }
    }


}