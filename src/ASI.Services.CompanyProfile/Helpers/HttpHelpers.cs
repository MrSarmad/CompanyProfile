using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Services.CompanyProfile.Helpers
{
    internal static class HttpHelpers
    {
        internal static async Task HandleResponseErrors(string path, HttpResponseMessage? responseMessage)
        {
            if (responseMessage == null)
                throw new CompanyProfileException($"No Response");

            if (responseMessage.IsSuccessStatusCode)
                return;

            async Task<string> GetContent() => await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            switch (responseMessage.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    throw new CompanyProfileNotFoundException(path);

                case System.Net.HttpStatusCode.BadRequest:
                    throw new CompanyProfileBadRequestException(path, await GetContent().ConfigureAwait(false));

                case System.Net.HttpStatusCode.InternalServerError:
                    throw new CompanyProfileInternalServerErrorException(path, await GetContent().ConfigureAwait(false));
            }

            throw new CompanyProfileException($"Error calling CompanyProfile service '{path}': {(int)responseMessage.StatusCode}- {await GetContent().ConfigureAwait(false)}");
        }
    }
}
