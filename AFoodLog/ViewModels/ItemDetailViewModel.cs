using System;

namespace AFoodLog
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }

    public class Item
    {
        public string Text
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
    }
}
