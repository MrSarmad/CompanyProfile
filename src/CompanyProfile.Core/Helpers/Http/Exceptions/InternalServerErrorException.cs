using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string url, string reason, string content)
            : base($"Internal Server Error occurred. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }   
}
