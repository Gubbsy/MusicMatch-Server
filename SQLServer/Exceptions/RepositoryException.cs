using System;


namespace SQLServer.Exceptions
{
    public class RepositoryException : Exception
    {
        public string[] ErrorMessages { get; }

        public RepositoryException(string[] errorMessage) : base("A repository has thrown an exception ")
        {
            ErrorMessages = errorMessage;
        }

        public RepositoryException(string[] errorMessage, Exception inner) : base("A repository has thrown an exception: ", inner)
        {
            ErrorMessages = errorMessage;
        }
    }
}
