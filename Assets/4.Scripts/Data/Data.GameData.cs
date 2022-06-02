using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Isometric.Data
{

    [Serializable]
    public class ItemDB : Data
    {
        public int id;
        public string name;
        public string description;
        public int size;
        public int count;
        public int slot;
        public Enums.ItemType itemType;
    }

    public class WeaponDB : ItemDB 
    {
        public Enums.WeaponType weaponType;
        public int attack;
        
    }

    public class ArmorDB : ItemDB
    {
        public Enums.ArmorType armorType;
        public int defense;
    }

    public class ConsumableDB : ItemDB
    {
        public Enums.Consumable consumableType;
        public float hp;
        public Enums.BuffType buffType;
    }
    public class UseableDB : ItemDB
    {
        public Enums.Useable useableType;
    }

    [Serializable]
    public class ItemData : ILoaderList<ItemDB>
    {
        public List<WeaponDB> WeaponDB = new List<WeaponDB>();
        public List<ArmorDB> ArmorDB = new List<ArmorDB>();
        public List<ConsumableDB> ConsumableDB = new List<ConsumableDB>();
        public List<UseableDB> UseableDB = new List<UseableDB>();

        public List<ItemDB> MakeList()
        {
            List<ItemDB> ItemData = new List<ItemDB>();
            foreach(ItemDB item in WeaponDB)
            {
                item.itemType = Enums.ItemType.Weapon;
                ItemData.Add(item);
            }
            foreach (ItemDB item in ArmorDB)
            {
                item.itemType = Enums.ItemType.Armor;
                ItemData.Add(item);
            }
            foreach (ItemDB item in ConsumableDB)
            {
                item.itemType = Enums.ItemType.Consumable;
                ItemData.Add(item);
            }
            foreach (ItemDB item in UseableDB)
            {
                item.itemType = Enums.ItemType.Useable;
                ItemData.Add(item);
            }

            return ItemData;
        }
    }
}