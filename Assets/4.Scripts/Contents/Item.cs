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
                Debug.Log("templateID�� �ش��ϴ� Item Info�� ã�� �� ����");
            }

            infos.itemDB = Managers.Data.ItemDBDict.FirstOrDefault(x => x.Value.itemTemplateID == templateID && x.Value.count < infos.itemInfo.maxCount).Value;
              
            if (infos.itemDB.Equals(default(KeyValuePair)))
            {
                infos.itemDB = null;
                Debug.Log("�ش��ϴ� DB ã�� �� ����");
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
            //itemDB���� �˻��� �� ����, �������ִ� �������� ȹ���Ѱ���̴�.
            //��� ��� ���� non-Stackable�� �������ϼ���, Stackable ������ �ִ밳����ŭ ���������� ���� �ִ�.

            foreach(KeyValuePair<int, ItemDB> kv  in Managers.Data.ItemDBDict)
            {
                if(kv.Value.itemTemplateID == templateID && kv.Value.count < infos.itemInfo.maxCount)
                {
                    if(kv.Value.count + count < infos.itemInfo.maxCount)
                    {
                        //�ֿ� �������� ���ص� �ִ밳���� �ѱ��� �ʴ� ���
                        kv.Value.count += count;
                    }

                    //�ٸ���쿡�� ����Ѵ�.
                }
            }
            return null;
            
        }

        public static Item CollectNewItem(ItemInfo iteminfo, int count = 1)
        {
            //TODO
            // DB�� �����Ƿ� ���ο� ���������� �ν��Ͽ� ȹ��� ���ÿ� DB�� ���� �� InventoryManager�� �˸�
            if(iteminfo == null)
            {
                Debug.Log(iteminfo);
                return null;
            }
            int? slot =  Managers.Inven.FindEmptySlot();
            if(slot == null)
            {
                Debug.Log("�κ��丮�� ������� ����");
                return null;
            }

            //TODO
            //FindEmptySlot�� ��ȯ���� dbid�� �����ϹǷ�, �״�� key�� �޾Ƽ� ��ųʸ��� add
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

            //newItemDB.itemDbid = ���ο� dbid �����ϴ� �Լ� �ʿ�

            return null;
            

            
        }

        
        public static ItemDB ItemToDB(Item item)
        {
            ItemDB itemDB = new ItemDB();
            int? newSlot = Managers.Inven.FindEmptySlot();
            if (newSlot == null)
            {
                Debug.Log("�κ��丮 ����");
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
                //DB�� ���� ������ �ҷ����� �õ�
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
                // ������ ������ �ȵȰ��
                return null;
            }

            item.ItemDbId = newItem.itemDbID;
            return item;
        }

    }
    //Ŭ���̾�Ʈ�� ����ִ°�
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
            //DBid�� null�� �Է¹��� ��� DB ��ȸ�� �ǹ̰� �ƴϴ�
            if (dbID != null)
            {
                ItemDbId = (int)dbID;
            }
            ItemInfo itemInfo = new WeaponInfo();
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);
            if (itemInfo.itemType != Enums.ItemType.Weapon)
            {
                //ũ����
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
                //ũ���� �Ƹ� database�� �߸��Ǿ��� ���ɼ� ����
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
                //ũ���� �Ƹ� database�� �߸��Ǿ��� ���ɼ� ����
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
                //ũ���� �Ƹ� database�� �߸��Ǿ��� ���ɼ� ����
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