using System;

namespace CompanyProfile.Core.Http.Exceptions
{
    public sealed class UnauthorizedException : Exception
    {
        public UnauthorizedException()
            : base($"401 Unauthorized")
        {

        }
    }
}
