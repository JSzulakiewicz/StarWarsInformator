using System;
using System.Runtime.Serialization;

namespace SwApiClient
{
    [Serializable]
    public class DataParsingException : Exception
    {
        public DataParsingException(string message) : base(message)
        {
        }

        public DataParsingException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}