using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{

    [Serializable]
    public class ItemDB : Data
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
    public class WeaponDB : ItemDB 
    {
        public Enums.WeaponType weaponType;
        public int attack;
        
    }
    [Serializable]
    public class ArmorDB : ItemDB
    {
        public Enums.ArmorType armorType;
        public int defense;
    }
    [Serializable]
    public class ConsumableDB : ItemDB
    {
        
        public Enums.ConsumableType consumableType;
        public float hp;
        public Enums.BuffType buffType;
    }
    [Serializable]
    public class UseableDB : ItemDB
    {
        public Enums.UseableType useableType;
    }

    [Serializable]
    public class ItemDBData : ILoaderDict<int, ItemDB>
    {
        public List<WeaponDB> WeaponDB = new List<WeaponDB>();
        public List<ArmorDB> ArmorDB = new List<ArmorDB>();
        public List<ConsumableDB> ConsumableDB = new List<ConsumableDB>();
        public List<UseableDB> UseableDB = new List<UseableDB>();

        public Dictionary<int, ItemDB> MakeDict()
        {
            Debug.Log("ItemDBData MakeDict 실행");
            Dictionary<int, ItemDB> ItemDB = new Dictionary<int, ItemDB>();
            foreach(ItemDB item in WeaponDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");

                item.itemType = Enums.ItemType.Weapon;
                ItemDB.Add(item.itemDbID, item);
            }
            foreach (ItemDB item in ArmorDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");
                item.itemType = Enums.ItemType.Armor;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemDB item in ConsumableDB)
            {
                Debug.Log("WeaponDB 조회");
                Debug.Log(item.itemDbID + "DB ID" + item + "아이템");
                item.itemType = Enums.ItemType.Consumable;
                ItemDB.Add(item.itemDbID, item);

            }
            foreach (ItemDB item in UseableDB)
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