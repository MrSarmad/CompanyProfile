using ASI.Services.Algolia;
using ASI.Services.Algolia.Client;
using CompanyProfile.Core.Configuration;
using CompanyProfile.Core.Search.Models;
using System;
using System.Collections.Generic;

namespace CompanyProfile.Algolia
{
    public class AlgoliaIndexNameProvider : DictionaryIndexNameProvider
    {
        private readonly IConfiguration _config;

        public AlgoliaIndexNameProvider(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public override Dictionary<Type, string> IndexNames => new Dictionary<Type, string>
        {
            [typeof(SearchMyEntity)] = "myEntity",
        };

        public override bool FallbackToTypeName => false;

        public override string GetIndexName<T>()
        {
            return $"{_config.EnvironmentName?.ToLower() ?? "local"}_{base.GetIndexName<T>()}";
        }
    }
}
