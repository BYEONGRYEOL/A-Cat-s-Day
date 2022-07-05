using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric.Data
{
    public class InventoryManager
    {
        public Dictionary<int, Item> Items { get; } = new Dictionary<int, Item>();

        public void Clear()
        {
            Items.Clear();
        }
        public void Init()
        {

        }
        public void LoadItem()
        {
            foreach (KeyValuePair<int, Item> kv in Items)
            {
                Items.Add(kv.Key, kv.Value);
            }
            Debug.Log(Items.Count + "개의 아이템 인벤토리에 로드");
        }
        public void Add(Item item)
        {
            Items.Add(item.ItemDbId, item);
        }

        public Item Get(int itemDbid)
        {
            Item item = null;
            Items.TryGetValue(itemDbid, out item);
            return item;
        }

        public Item Find(Func<Item, bool> condition)
        {
            foreach(Item item in Items.Values)
            {
                if (condition.Invoke(item))
                {
                    return item;
                }
            }
            //아이템을 못찾은경우
            return null;
        }
    }
}
