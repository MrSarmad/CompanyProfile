using CompanyProfile.Core.Models;
using System;

namespace CompanyProfile.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(long id, IEntity entity) : base($"{entity.GetType().Name}: {id} was not found")
        {
        }

        public EntityNotFoundException(long id, Type entity) : base($"{entity.Name}: {id} was not found")
        {
        }
    }
}
