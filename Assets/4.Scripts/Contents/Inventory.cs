using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Isometric.Data
{
    public class Inventory
    {
        Dictionary<int, Item> items = new Dictionary<int, Item>();

        public void Add(Item item)
        {
            items.Add(item.ItemDbId, item);
        }

        public Item Get(int itemDbid)
        {
            Item item = null;
            items.TryGetValue(itemDbid, out item);
            return item;
        }

        public Item Find(Func<Item, bool> condition)
        {
            foreach(Item item in items.Values)
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
