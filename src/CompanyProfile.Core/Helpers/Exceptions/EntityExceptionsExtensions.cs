using CompanyProfile.Core.Models;

namespace CompanyProfile.Core.Exceptions
{
    public static class EntityExceptionsExtensions
    {
        public static T ValidateFound<T>(this T? entity, long id)
            where T : class, IEntity
        {
            if (entity == null)
                throw new EntityNotFoundException(id, typeof(T));
            return entity;
        }
    }
}
