namespace ASI.Services.CompanyProfile
{
    public class CompanyProfileBadRequestException : CompanyProfileException
    {
        public CompanyProfileBadRequestException(string path, string message) : base($"CompanyProfile 400 Bad Request at '{path}': {message}") { }
    }
}
