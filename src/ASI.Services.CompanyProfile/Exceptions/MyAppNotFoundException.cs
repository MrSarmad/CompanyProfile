namespace ASI.Services.CompanyProfile
{
    public class CompanyProfileNotFoundException : CompanyProfileException
    {
        public CompanyProfileNotFoundException(string path) : base($"404 Not Found: {path}") { }
    }
}
