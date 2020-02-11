using System;


namespace SQLServer.Exceptions
{
    public class RepositoryException : Exception
    {
        public string[] ErrorMessages { get; }

        public RepositoryException(string errorMessage) : this(errorMessage, null)
        {
        }

        public RepositoryException(string[] errorMessages) : this(errorMessages, null)
        {
        }

        public RepositoryException(string errorMessage, Exception inner) : this(new string[] { errorMessage }, inner)
        {
        }

        public RepositoryException(string[] errorMessage, Exception inner) : base("A repository has thrown an exception: ", inner)
        {
            ErrorMessages = errorMessage;
        }
    }
}
