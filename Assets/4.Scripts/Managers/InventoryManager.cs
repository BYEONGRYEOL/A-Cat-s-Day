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
            LoadItem();
        }

        public int? FindEmptySlot()
        {
            var dbIDs = Items.Keys;
            for(int i = 1; i < 21; i++)
            {
                if (!dbIDs.Contains(i))
                {
                    return i;
                }
            }
            return null;
        }
        public void LoadItem()
        {
            foreach (ItemDB itemDB in Managers.Data.ItemDBDict.Values)
            {
                Add(Item.GetItemFromDB(itemDB));
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
