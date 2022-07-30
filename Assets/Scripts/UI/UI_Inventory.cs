using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;
using System.Linq;

namespace Isometric.UI
{
    public class UI_Inventory : UI_Base
    {
        public List<UI_Inventory_Slot> Slots { get; set; } = new List<UI_Inventory_Slot>();


        private void Awake()
        {
            Init();
            RefreshSlot();
        }
        public override void Init()
        {
            Managers.Inven.LoadItem();
            Slots.Clear();
            SlotInit();

            
            
        }

        public void SlotInit()
        {
            GameObject grid = transform.Find("Item_Grid").gameObject;
            foreach (Transform child in grid.transform)
                Destroy(child.gameObject);

            //인벤토리 슬롯의 총 개수를 임의로 20개로 정함
            for (int i = 1; i < 21; i++)
            {
                GameObject go = Managers.Resource.Instantiate("UI/Items/Inventory_Slot", parent: grid.transform);
                UI_Inventory_Slot slot = go.GetComponent<UI_Inventory_Slot>();
                //초기화 
                slot.HasItem = false;

                Slots.Add(slot);
            }
            foreach (Item item in Managers.Inven.Items.Values)
            {
                Slots[item.Slot].HasItem = true;
            }
        }

        public void RefreshSlot()
        {
            List<Item> items = Managers.Inven.Items.Values.ToList();

            Debug.Log(items.Count);
            Debug.Log(items[1].Name);

            items.Sort((left, right) => left.Slot.CompareTo(right.Slot));
            for(int i=0 ; i<items.Count; i++)
            {
                Debug.Log("Items List Counts :: " + items.Count);
                Debug.Log("RefreshSlot function ItemName is " + items[i].Name);
                Slots[items[i].Slot].SetItem(items[i].ItemTemplateId, items[i].Count);
            }
        }
    }
}