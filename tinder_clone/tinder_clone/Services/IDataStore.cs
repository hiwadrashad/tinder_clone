using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tinder_clone.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<T> GetItemAsyncBynameAndPassword(string username, string password);

        List<T> ReturnList();
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
