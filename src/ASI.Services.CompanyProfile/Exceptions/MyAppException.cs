using System;
using System.Runtime.Serialization;

namespace ASI.Services.CompanyProfile
{
    public class CompanyProfileException : Exception
    {
        public CompanyProfileException()
        {
        }

        public CompanyProfileException(string? message) : base(message)
        {
        }

        public CompanyProfileException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected CompanyProfileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
