using System;

namespace Work_Site.BLL.Exceptions
{
    public class InvalidResumeException : Exception
    {
        public InvalidResumeException() { }

        public InvalidResumeException(string message) : base(message) { }

        public InvalidResumeException(string message, Exception inner):base(message, inner) { }
    }
}