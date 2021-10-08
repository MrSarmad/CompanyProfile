namespace ASI.Services.CompanyProfile
{
    public class MyEntityNotFoundException : CompanyProfileException
    {
        public MyEntityNotFoundException(long id) : base($"Entity Id {id} Not Found") { }
    }
}
