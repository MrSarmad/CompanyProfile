using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class BadRequestException : Exception
    {
        public BadRequestException(string url, string reason, string content)
            :base($"BadRequest. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }
}
