using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{

    [Serializable]
    public class ItemDB : Data
    {
        public int itemDbid;
        public int itemTemplateid;
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
            Dictionary<int, ItemDB> ItemDB = new Dictionary<int, ItemDB>();
            foreach(ItemDB item in WeaponDB)
            {
                item.itemType = Enums.ItemType.Weapon;
                ItemDB.Add(item.itemDbid, item);
            }
            foreach (ItemDB item in ArmorDB)
            {
                item.itemType = Enums.ItemType.Armor;
                ItemDB.Add(item.itemDbid, item);

            }
            foreach (ItemDB item in ConsumableDB)
            {
                item.itemType = Enums.ItemType.Consumable;
                ItemDB.Add(item.itemDbid, item);

            }
            foreach (ItemDB item in UseableDB)
            {
                item.itemType = Enums.ItemType.Useable;
                ItemDB.Add(item.itemDbid, item);

            }

            return ItemDB;
        }
    }
}