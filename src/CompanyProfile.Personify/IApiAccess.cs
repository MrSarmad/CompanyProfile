using ASI.Contracts.CompanyProfile.CompanyProfile.XMLModel;

using System.Threading.Tasks;

namespace CompanyProfile.Personify
{
    public interface IApiAccess
    {
        Task<CompanyGeneralInfo> GetGeneralInfo(StoredProcedureRequest req);
        Task<bool> UpdateAboutUs(StoredProcedureRequest req);
    }
}