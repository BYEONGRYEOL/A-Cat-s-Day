using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Isometric.Data
{

    public struct ItemInfos
    {
        public ItemInfo itemInfo;
        public ItemDB itemDB;
    }

    public class Item
    {
        public ItemDB itemDB { get; } = new ItemDB();
        public ItemInfo itemInfo { get; } = new ItemInfo();

        public int ItemDbId { get => itemDB.itemDbID; set => itemDB.itemDbID = value; }
        public int ItemTemplateId { get => itemDB.itemTemplateID; set => itemDB.itemTemplateID = value; }
        public int Count { get => itemDB.count; set => itemDB.count = value; }
        public string Name { get => itemInfo.name; set => itemInfo.name = value; }
        public string Description { get => itemInfo.description; set => itemInfo.description = value; } 

        


        public Enums.ItemType ItemType { get; private set; }
        public bool Stackable { get; protected set; }

        public Item(Enums.ItemType itemType)
        {
            ItemType = itemType;
        }
        
        public static ItemInfos CheckItem(int templateID)
        {
            ItemInfos infos = new ItemInfos();
            Managers.Data.ItemInfoDict.TryGetValue(templateID, out infos.itemInfo);
            if(infos.itemInfo == null)
            {
                Debug.Log("templateID에 해당하는 Item Info를 찾을 수 없음");
            }

            infos.itemDB = Managers.Data.ItemDBDict.FirstOrDefault(x => x.Value.itemTemplateID == templateID && x.Value.count < infos.itemInfo.maxCount).Value;
              
            if (infos.itemDB.Equals(default(KeyValuePair)))
            {
                infos.itemDB = null;
                Debug.Log("해당하는 DB 찾을 수 없음");
            }
            return infos;
        }
        public static Item CollectItem(int templateID, int count = 1)
        {
            ItemInfos infos = CheckItem(templateID);
            if(infos.itemDB == null)
            {
                CollectNewItem(infos.itemInfo, count);
            }

            //TODO
            //itemDB에서 검색이 된 경우로, 가지고있는 아이템을 획득한경우이다.
            //장비 등과 같이 non-Stackable한 아이템일수도, Stackable 하지만 최대개수만큼 가지고있을 수도 있다.

            foreach(KeyValuePair<int, ItemDB> kv  in Managers.Data.ItemDBDict)
            {
                if(kv.Value.itemTemplateID == templateID && kv.Value.count < infos.itemInfo.maxCount)
                {
                    if(kv.Value.count + count < infos.itemInfo.maxCount)
                    {
                        //주운 아이템을 더해도 최대개수를 넘기지 않는 경우
                        kv.Value.count += count;
                    }

                    //다른경우에도 써야한다.
                }
            }
            return null;
            
        }

        public static Item CollectNewItem(ItemInfo iteminfo, int count = 1)
        {
            //TODO
            // DB에 없으므로 새로운 아이템으로 인식하여 획득과 동시에 DB에 저장 후 InventoryManager에 알림
            if(iteminfo == null)
            {
                Debug.Log(iteminfo);
                return null;
            }
            int? slot =  Managers.Inven.FindEmptySlot();
            if(slot == null)
            {
                Debug.Log("인벤토리에 빈공간이 없음");
                return null;
            }

            //TODO
            //FindEmptySlot의 반환값이 dbid와 동일하므로, 그대로 key로 받아서 딕셔너리에 add
            Item item = null;
            switch (iteminfo.itemType)
            {
                case Enums.ItemType.Weapon:
                    item = new Weapon(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Armor:
                    item = new Armor(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Consumable:
                    item = new Consumable(iteminfo.itemTemplateid);
                    break;
                case Enums.ItemType.Useable:
                    item = new Useable(iteminfo.itemTemplateid);
                    break;
            }

            //newItemDB.itemDbid = 새로운 dbid 생성하는 함수 필요

            return null;
            

            
        }

        
        public static ItemDB ItemToDB(Item item)
        {
            ItemDB itemDB = new ItemDB();
            int? newSlot = Managers.Inven.FindEmptySlot();
            if (newSlot == null)
            {
                Debug.Log("인벤토리 꽉참");
                return null;
            }
            else
            {
                itemDB.slot = (int)newSlot;
            }
            switch (item.ItemType)
            {
                case Enums.ItemType.Weapon:

                    itemDB.itemType = item.ItemType;
                    Weapon weapon = (Weapon)item;
                    WeaponDB weaponDB = (WeaponDB)itemDB;
                    weaponDB.weaponType = weapon.WeaponType;
                    weaponDB.attack = weapon.Attack;
                    break;
                case Enums.ItemType.Armor:

                    itemDB.itemType = item.ItemType;
                    Armor armor = (Armor)item;
                    ArmorDB armorDB = (ArmorDB)itemDB;
                    armorDB.armorType = armor.ArmorType;
                    armorDB.defense = armor.Defense;
                    break;
                case Enums.ItemType.Consumable:

                    itemDB.itemType = item.ItemType;
                    Consumable consumable = (Consumable)item;
                    ConsumableDB consumableDB = (ConsumableDB)itemDB;
                    consumableDB.consumableType = consumable.ConsumableType;
                    consumableDB.buffType = consumable.BuffType;
                    break;

                case Enums.ItemType.Useable:

                    itemDB.itemType = item.ItemType;
                    Useable useable = (Useable)item;
                    UseableDB useableDB = (UseableDB)itemDB;
                    useableDB.useableType = useable.Useabletype;

                    break;
            }
            itemDB.name = item.Name;
            itemDB.count = item.Count;
            itemDB.itemDbID = item.ItemDbId;
            itemDB.description = item.Description;
            
            return itemDB;
        }
        public static Item GetItemFromDB(ItemDB itemDB)
        {
            Item item = null;
            ItemDB newItem = null;
            Managers.Data.ItemDBDict.TryGetValue(itemDB.itemDbID, out newItem);

            if(newItem == null)
            {
                //DB에 없는 아이템 불러오기 시도
                return null;
            }

            switch (newItem.itemType)
            {
                case Enums.ItemType.Weapon:
                    item = new Weapon(newItem.itemTemplateID, newItem.itemDbID);
                    break;
                case Enums.ItemType.Armor:
                    item = new Armor(newItem.itemTemplateID, newItem.itemDbID);
                    break;
                case Enums.ItemType.Consumable:
                    item = new Consumable(newItem.itemTemplateID, newItem.itemDbID);
                    break;
                case Enums.ItemType.Useable:
                    item = new Useable(newItem.itemTemplateID, newItem.itemDbID);
                    break;
            }
            
            if(item == null)
            {
                // 아이템 생성이 안된경우
                return null;
            }

            item.ItemDbId = newItem.itemDbID;
            return item;
        }

    }
    //클라이언트가 들고있는거
    public class Weapon : Item 
    {
        public Enums.WeaponType WeaponType { get; private set; }
        public int Attack { get; private set; }
        public Weapon(int templateid, int? dbID = null) : base(Enums.ItemType.Weapon)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            //DBid가 null로 입력받은 경우 DB 조회의 의미가 아니다
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ItemInfo itemInfo = new WeaponInfo();
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
            }
        }
    }
    

    public class Armor : Item
    {
        public Enums.ArmorType ArmorType { get; private set; }
        public int Defense { get; private set; }
        public Armor(int templateid, int? dbID = null) : base(Enums.ItemType.Armor)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ItemInfo iteminfo = new ArmorInfo();
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
        public Enums.BuffType BuffType { get; private set; }
        public Consumable(int templateid, int? dbID = null) : base(Enums.ItemType.Consumable)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ItemInfo iteminfo = new ConsumableInfo();
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
            BuffType = newData.buffType;
            HP = newData.hp;
            Stackable = false;
        }
    }
    public class Useable : Item
    {
        public Enums.UseableType Useabletype { get; private set;}
        public int Defense { get; private set; }
        public Useable(int templateid, int? dbID = null) : base(Enums.ItemType.Useable)
        {
            Init(templateid, dbID);
        }
        void Init(int templateid, int? dbID = null)
        {
            if(dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ItemInfo iteminfo = new UseableInfo();
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
 
}