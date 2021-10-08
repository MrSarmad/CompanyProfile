namespace ASI.Services.CompanyProfile
{
    public class CompanyProfileInternalServerErrorException : CompanyProfileException
    {
        public CompanyProfileInternalServerErrorException(string path, string message) : base($"CompanyProfile  500 Internal Server Error at '{path}': {message}") { }
    }
}
