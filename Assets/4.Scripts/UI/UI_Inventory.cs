using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Isometric.Data;

namespace Isometric.UI
{
    public class UI_Inventory : UI_Base
    {
        public List<UI_Inventory_Slot> Slots { get; } = new List<UI_Inventory_Slot>();


        private void Awake()
        {
            Init();
        }
        public override void Init()
        {
            Slots.Clear();
            GameObject grid = transform.Find("Item_Grid").gameObject;
            foreach(Transform child in grid.transform)
                Destroy(child.gameObject);

            //�κ��丮 ������ �� ������ ���Ƿ� 20���� ����
            for ( int i = 0; i < 20; i++)
            {
                GameObject go = Managers.Resource.Instantiate("UI/Inventory_Slot");
                UI_Inventory_Slot slot = go.GetComponent<UI_Inventory_Slot>();
                Slots.Add(slot);
            }
        }

        public void RefreshSlot()
        {
            //List<Item> items = Managers.Inven.Items.
        }
       

    }

}