using System;

namespace SwApiClient
{
    [Serializable]
    public class ConnectionErrorException : Exception
    {
        public ConnectionErrorException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public ConnectionErrorException(string message)
            : base(message)
        { }
    }
}