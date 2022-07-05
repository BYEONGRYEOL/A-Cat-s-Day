using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Data
{
<<<<<<< HEAD
    public struct ItemInfos
    {
        public ItemInfo itemInfo;
        public ItemDB itemDB;
    }



    public class Item
    {
        public ItemDB itemDBInfo { get; } = new ItemDB();
        public int ItemDbId { get => itemDBInfo.itemDbid; set => itemDBInfo.itemDbid = value; }
        public int ItemTemplateId { get => itemDBInfo.itemTemplateid; set => itemDBInfo.itemTemplateid = value; }
        
        public int Count { get => itemDBInfo.count; set => itemDBInfo.count = value; }
=======
    public class Item
    {
        public ItemDB itemDBInfo { get; } = new ItemDB();

        public int ItemDbId { get => itemDBInfo.itemDbid; set => itemDBInfo.itemDbid = value; }
        public int ItemTemplateId { get => itemDBInfo.itemTemplateid; set => itemDBInfo.itemTemplateid = value; }
        public int Count { get => itemDBInfo.count; set => itemDBInfo.count = value; }

>>>>>>> 03113eccbaf095a8b52cc804a6fe5aa3ca7d7ea3
        public Enums.ItemType ItemType { get; private set; }
        public bool Stackable { get; protected set; }

        public Item(Enums.ItemType itemType)
        {
            ItemType = itemType;
        }
<<<<<<< HEAD
        public static void CheckItem(int templateID)
        {
            
        }
        public static Item GetItemFromDB(ItemDB itemDB)
        {
            Item item = null;
            ItemDB newItem = null;
            Managers.Data.ItemDBDict.TryGetValue(itemDB.itemDbid, out newItem);

            if(newItem == null)
            {
                //DB에 없는 아이템 불러오기 시도
                return null;
            }
            switch (newItem.itemType)
            {
                case Enums.ItemType.Weapon:
                    item = new Weapon(itemDB.itemTemplateid);
                    break;
                case Enums.ItemType.Armor:
                    item = new Armor(itemDB.itemTemplateid);
                    break;
                case Enums.ItemType.Consumable:
                    item = new Consumable(itemDB.itemTemplateid);
                    break;
                case Enums.ItemType.Useable:
                    item = new Useable(itemDB.itemTemplateid);
                    break;
            }
            
            if(item == null)
            {
                // 아이템 생성이 안된경우
                return null;
            }
            item.ItemDbId = itemDB.itemDbid;
            return item;
        }
        

    }

    //클라이언트가 들고있는거
=======

    }

>>>>>>> 03113eccbaf095a8b52cc804a6fe5aa3ca7d7ea3
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
                //크래쉬
                return;
            }

            WeaponInfo newData = (WeaponInfo)itemInfo;
            {
                ItemTemplateId = newData.itemTemplateid;
                Count = 1;
                WeaponType = newData.weaponType;
                Attack = newData.attack;
                Stackable = false;
<<<<<<< HEAD

            }
        }
    }
    public class Armor : Item
    {
        public Enums.ArmorType ArmorType { get; private set; }
        public int Defense { get; private set; }
        public Armor(int templateid) : base(Enums.ItemType.Armor)
        {
            Init(templateid);
        }
        void Init(int templateid)
        {
            ItemInfo iteminfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out iteminfo);
            if(iteminfo.itemType != Enums.ItemType.Armor)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }
            ArmorInfo newData = (ArmorInfo)iteminfo;
            ItemTemplateId = newData.itemTemplateid;
            Count = 1;
            ArmorType = newData.armorType;
            Defense = newData.defense;
            Stackable = false;
        }
    }
    public class Consumable : Item
    {
        public Enums.ConsumableType ConsumableType { get; private set; }
        public int HP { get; private set; }
        public Consumable(int templateid) : base(Enums.ItemType.Consumable)
        {
            Init(templateid);
        }
        void Init(int templateid)
        {
            ItemInfo iteminfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out iteminfo);
            if (iteminfo.itemType != Enums.ItemType.Consumable)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }
            ConsumableInfo newData = (ConsumableInfo)iteminfo;
            ItemTemplateId = newData.itemTemplateid;
            Count = 1;
            ConsumableType = newData.consumableType;
            HP = newData.hp;
            Stackable = false;
        }
    }
    public class Useable : Item
    {
        public Enums.UseableType Useabletype { get; private set; }
        public int Defense { get; private set; }
        public Useable(int templateid) : base(Enums.ItemType.Useable)
        {
            Init(templateid);
        }
        void Init(int templateid)
        {
            ItemInfo iteminfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out iteminfo);
            if (iteminfo.itemType != Enums.ItemType.Armor)
            {
                //크래쉬 아마 database가 잘못되었을 가능성 높음
                return;
            }
            UseableInfo newData = (UseableInfo)iteminfo;
            ItemTemplateId = newData.itemTemplateid;
            Count = 1;
            Useabletype = newData.useableType;
            Stackable = false;
        }
    }
=======
            }
        }
    }
    
>>>>>>> 03113eccbaf095a8b52cc804a6fe5aa3ca7d7ea3
}