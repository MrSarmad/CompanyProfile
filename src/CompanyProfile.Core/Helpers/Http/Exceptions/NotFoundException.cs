using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string url, string reason, string content)
            :base($"NotFound. Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }
}
