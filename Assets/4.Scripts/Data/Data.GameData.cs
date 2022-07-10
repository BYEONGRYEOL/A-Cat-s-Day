using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{

    [Serializable]
    public class ItemData : Data
    {
        public int itemDbID;
        public int itemTemplateID;
        public string name;
        public string description;
        public int count;
        public int slot;
        public Enums.ItemType itemType;
    }
    [Serializable]
    public class WeaponData : ItemData 
    {
        public Enums.WeaponType weaponType;
        public int attack;
        
    }
    [Serializable]
    public class ArmorData : ItemData
    {
        public Enums.ArmorType armorType;
        public int defense;
    }
    [Serializable]
    public class ConsumableData : ItemData
    {
        
        public Enums.ConsumableType consumableType;
        public float hp;
        public Enums.BuffType buffType;
    }
    [Serializable]
    public class UseableData : ItemData
    {
        public Enums.UseableType useableType;
    }

    [Serializable]
    public class ItemLoader : ILoaderDict<int, ItemData>
    {
        public List<WeaponData> WeaponDB = new List<WeaponData>();
        public List<ArmorData> ArmorDB = new List<ArmorData>();
        public List<ConsumableData> ConsumableDB = new List<ConsumableData>();
        public List<UseableData> UseableDB = new List<UseableData>();

        public Dictionary<int, ItemData> MakeDict()
        {
            Debug.Log("ItemDBData MakeDict 실행");
            Dictionary<int, ItemData> ItemDB = new Dictionary<int, ItemData>();
            foreach(ItemData item in WeaponDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");

                item.itemType = Enums.ItemType.Weapon;
                ItemDB.Add(item.itemDbID, item);
            }
            foreach (ItemData item in ArmorDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");
                item.itemType = Enums.ItemType.Armor;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemData item in ConsumableDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");
                item.itemType = Enums.ItemType.Consumable;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemData item in UseableDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");
                item.itemType = Enums.ItemType.Useable;
                ItemDB.Add(item.itemDbID, item);

            }

            return ItemDB;
        }
    }
}