using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class RequestTimeoutException : Exception
    {
        public RequestTimeoutException(string url, string reason, string content)
            : base($"Request Timeout. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }       
}
