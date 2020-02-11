using System;


namespace SQLServer.Exceptions
{
    public class ControllerException : Exception
    {
        public string[] ErrorMessages { get; }

        public ControllerException(string errorMessage) : this(errorMessage, null)
        {
        }

        public ControllerException(string[] errorMessages) : this(errorMessages, null)
        {
        }

        public ControllerException(string errorMessage, Exception inner) : this(new string[] { errorMessage }, inner)
        {
        }

        public ControllerException(string[] errorMessage, Exception inner) : base("A repository has thrown an exception: ", inner)
        {
            ErrorMessages = errorMessage;
        }
    }
}
