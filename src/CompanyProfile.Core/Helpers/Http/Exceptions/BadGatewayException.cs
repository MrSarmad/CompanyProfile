using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class BadGatewayException : Exception
    {
        public BadGatewayException(string url, string reason, string content)
            :base($"BadGateway. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }
}
