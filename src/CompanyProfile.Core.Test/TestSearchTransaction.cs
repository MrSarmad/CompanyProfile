using ASI.Services.Search.Indexing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyProfile.Core.Test
{

    public class TestSearchTransaction : ISearchTransaction
    {
        private int _count;

        public void Commit() { Commit(1); }
        public void Commit(int maxRetries)
        {

        }
        public Task CommitAsync() { return CommitAsync(1); }
        public Task CommitAsync(int maxRetries)
        {
            return Task.CompletedTask;
        }

        public List<object> Added { get; set; } = new();
        public List<object> Updated { get; set; } = new();
        public List<object> Removed { get; set; } = new();
        public List<object> PartialUpdated { get; set; } = new();
        public List<(List<long>, object)> PartialUpdatedById { get; set; } = new();
        public List<long> UpdatedIds { get; set; } = new();
        public List<object> UpdatedFields { get; set; } = new();

        public List<T> AddedOfType<T>() => Added.OfType<T>().ToList();
        public List<T> UpdatedOfType<T>() => Updated.OfType<T>().ToList();
        public List<T> RemovedOfType<T>() => Removed.OfType<T>().ToList();

        public ISearchTransaction Add<T>(T record) where T : class => AddRange(new[] { record });
        public ISearchTransaction Update<T>(T record) where T : class => UpdateRange(new[] { record });
        public ISearchTransaction Remove<T>(T record) where T : class => RemoveRange(new[] { record });


        public ISearchTransaction AddRange<T>(IEnumerable<T> records) where T : class
        {
            return Create(records);
        }

        public ISearchTransaction Create<T>(IEnumerable<T> records)
        {
            _count += records.Count();
            Added.AddRange(records.Cast<object>());
            return this;
        }

        public ISearchTransaction UpdateRange<T>(IEnumerable<T> records) where T : class
        {
            _count += records.Count();
            Updated.AddRange(records.Cast<object>());
            return this;
        }

        public ISearchTransaction UpdateRange<T>(IEnumerable<long> ids, object fields) where T : class
        {
            _count += ids.Count();
            UpdatedIds.AddRange(ids);
            UpdatedFields.Add(fields);
            return this;
        }

        public ISearchTransaction RemoveRange<T>(IEnumerable<T> records) where T : class
        {
            return Delete(records);
        }

        public ISearchTransaction AddRaw<T>(string type, long id, T record) where T : class
        {
            _count++;
            Added.Add(record);
            return this;
        }

        public ISearchTransaction UpdateRaw<T>(string type, long id, T record) where T : class
        {
            _count++;
            Updated.Add(record);
            return this;
        }

        public ISearchTransaction RemoveRaw<T>(string type, long id) where T : class
        {
            _count++;
            return this;
        }

        public ISearchTransaction Delete<T>(IEnumerable<T> records)
        {
            _count += records.Count();
            Removed.AddRange(records.Cast<object>());
            return this;
        }

        public ISearchTransaction ForceRefresh()
        {
            return this;
        }

        public ISearchTransaction RemoveRange<T>(IEnumerable<long> ids) where T : class
        {
            var removed = ids.Select(x => new { Id = x });
            _count += removed.Count();
            Removed.AddRange(removed.Cast<object>());
            return this;
        }

        public ISearchTransaction PartialUpdate<T>(object data) where T : class
        {
            PartialUpdated.Add(data);
            return this;
        }

        public ISearchTransaction PartialUpdateRange<T>(IEnumerable<long> ids, object data) where T : class
        {
            PartialUpdatedById.Add((ids.ToList(), data));
            return this;
        }
    }
}

