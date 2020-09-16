using System;

using tinder_clone.Models;

namespace tinder_clone.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item.Username;
            Item = item;
        }
    }
}
