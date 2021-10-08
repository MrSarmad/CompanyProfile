using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class UnknownErrorException : Exception
    {
        public UnknownErrorException(string url, string reason, string content)
            : base($"Unrecognized Error occurred. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }
}
