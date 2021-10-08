using ASI.Services.Access;

namespace CompanyProfile.Core.Context
{
    /// <summary>
    /// Represents information from the current authenticated user
    /// </summary>
    public interface IUserInformation 
    {
        long UserId { get; }
        string UserName { get; }
    }
}
