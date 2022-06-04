using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Data
{
    public class Item
    {
        public ItemDB itemDBInfo { get; } = new ItemDB();

        public int ItemDbId { get => itemDBInfo.itemDbid; set => itemDBInfo.itemDbid = value; }
        public int ItemTemplateId { get => itemDBInfo.itemTemplateid; set => itemDBInfo.itemTemplateid = value; }
        public int Count { get => itemDBInfo.count; set => itemDBInfo.count = value; }

        public Enums.ItemType ItemType { get; private set; }
        public bool Stackable { get; protected set; }

        public Item(Enums.ItemType itemType)
        {
            ItemType = itemType;
        }

    }

    public class Weapon : Item 
    {
        public Enums.WeaponType WeaponType { get; private set; }
        public int Attack { get; private set; }
        public Weapon(int templateid) : base(Enums.ItemType.Weapon)
        {
            Init(templateid);
        }
        void Init(int templateid)
        {
            ItemInfo itemInfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);
            if (itemInfo.itemType != Enums.ItemType.Weapon)
            {
                //Å©·¡½¬
                return;
            }

            WeaponInfo newData = (WeaponInfo)itemInfo;
            {
                ItemTemplateId = newData.itemTemplateid;
                Count = 1;
                WeaponType = newData.weaponType;
                Attack = newData.attack;
                Stackable = false;
            }
        }
    }
    
}