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

        //dbid 가 키야
        public Dictionary<int, Item> Items { get; } = new Dictionary<int, Item>();
        
        public int NewItemDbID()
        {
            return Managers.Data.ItemDBDict.Keys.Max() + 1;
        }
        public void Clear()
        {
            Items.Clear();
        }
        public void Init()
        {
            LoadItem();
            Debug.Log(Items.Count);
            Debug.Log("InventoryManager :: " + Items[1]);
        }

        public int? FindEmptySlot()
        {
            List<int> slots = new List<int>();
            foreach(Item items in Items.Values)
            {
                slots.Add(items.Slot);
            }
            for(int i = 1; i < 21; i++)
            {
                if (!slots.Contains(i))
                {
                    return i;
                }
            }
            return null;
        }
        public void LoadItem()
        {
            Debug.Log(Managers.Data.ItemDBDict.Count + "DataManager의 ItemDB딕셔너리 개수");
            foreach (ItemData itemDB in Managers.Data.ItemDBDict.Values)
            {
                if (!Items.ContainsKey(itemDB.itemDbID))
                {
                    Debug.Log("Inventory Manager에 Item이 포함되어있지 않은 경우에만 추가");
                    Add(Item.GetItemFromDB(itemDB));
                }
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
