using System;
using System.Net.Http;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class TailorUnavailableException : Exception
    {
        public TailorUnavailableException(string url, HttpRequestException inner)
            : base($"The Tailor service is unavailable. HttpRequestException:'{inner.Message}'. See InnerException for details. Url: {url}", inner)
        {

        }
    }
}
