using System;

namespace CompanyProfile.Core.Exceptions
{
    public sealed class MyEntityUpdateException : Exception
    {
        public string Name { get; }

        public MyEntityUpdateException(string name)
        {
            Name = name;
        }        
    }
}
