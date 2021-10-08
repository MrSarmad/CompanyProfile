using CompanyProfile.Core.Http.Exceptions;
using System.Net.Http;
using System.Threading.Tasks;

namespace CompanyProfile.Core.Http
{
    public static class HttpHelpers
    {
        public static async Task HandleStatusCode(string url, HttpResponseMessage response, string method)
        {
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content?.ReadAsStringAsync()!;
            
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.InternalServerError: throw new InternalServerErrorException(url, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.NotFound: throw new NotFoundException(url, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.BadRequest: throw new BadRequestException(url, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.BadGateway: throw new BadGatewayException(url, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.MethodNotAllowed: throw new MethodNotAllowedException(url, method, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.RequestTimeout: throw new RequestTimeoutException(url, response.ReasonPhrase, content);
                case System.Net.HttpStatusCode.Unauthorized: throw new UnauthorizedException();
                default: throw new UnknownErrorException(url, response.ReasonPhrase, content);
            }
        }
    }
}
