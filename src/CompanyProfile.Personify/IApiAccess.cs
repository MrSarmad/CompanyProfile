using System.Threading.Tasks;

namespace CompanyProfile.Personify
{
    public interface IApiAccess
    {
        Task<string> GetBasicInfo();
    }
}