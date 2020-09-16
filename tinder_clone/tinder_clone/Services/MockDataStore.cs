using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tinder_clone.Models;
using System.IO;

namespace tinder_clone.Services
{
    public class MockDataStore : IDataStore<Item>
    {
       static  readonly List<Item> items;

       static MockDataStore()
        {
  
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Username = "Trieuvan", Password="JackFruit7", Email="trieuvan@outlook.com", PhoneNumber="0670696542", UploadedImage=File.ReadAllBytes("test1.png"), Matches=new Dictionary<string, bool>(), telephonenumbers=new List<string>(), MatchNames=new List<string>(), SuperLikes=new List<string>(), Latitude=52.370216, Longitude=4.895168, Distance=50  },
                new Item { Id = Guid.NewGuid().ToString(), Username = "Amaranth", Password="Sophisticated10", Email="amaranth@yahoo.ca", PhoneNumber="0697973709", UploadedImage=File.ReadAllBytes("test3.jpg"), Matches=new Dictionary<string, bool>(), telephonenumbers=new List<string>(), MatchNames=new List<string>(), SuperLikes=new List<string>(), Latitude=43.653225, Longitude=-79.383186, Distance=50  },
                new Item { Id = Guid.NewGuid().ToString(), Username = "Chris", Password="UninterestedLook11", Email="chrisj@yahoo.com", PhoneNumber="0677030653", UploadedImage=File.ReadAllBytes("test4.jpg"), Matches=new Dictionary<string, bool>(), telephonenumbers=new List<string>(), MatchNames=new List<string>(), SuperLikes=new List<string>(), Latitude=52.370216, Longitude=4.895168, Distance=50  },
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
             return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public List<Item> ReturnList()
        {
            return items;
        }


        public async Task<Item> GetItemAsyncBynameAndPassword(string Username, string Password)
        {
            return await Task.FromResult(items.Where(s => s.Username == Username && s.Password == Password).FirstOrDefault());
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}