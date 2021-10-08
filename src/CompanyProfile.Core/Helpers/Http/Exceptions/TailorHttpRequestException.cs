using System;
using System.Net.Http;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class TailorHttpRequestException : Exception
    {
        public TailorHttpRequestException(string url, HttpRequestException inner)
            : base($"HttpRequestException:'{inner.Message}'. See InnerException for details. Url: {url}", inner)
        {

        }
    }
}
