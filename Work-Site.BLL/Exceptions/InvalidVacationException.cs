using System;

namespace Work_Site.BLL.Exceptions
{
    public class InvalidVacationException:Exception
    {
        public InvalidVacationException() { }

        public InvalidVacationException(string message) : base(message) { }

        public InvalidVacationException(string message, Exception inner):base(message, inner) { }
    }
}