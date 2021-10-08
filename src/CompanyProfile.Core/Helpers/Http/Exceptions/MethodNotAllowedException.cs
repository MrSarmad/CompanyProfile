using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException(string url, string method, string reason, string content)
            :base($"MethodNotAllowed. Method: '{method}' Reason: '{reason}' Content: '{content}' Url: {url}")
        {

        }
    }
}
