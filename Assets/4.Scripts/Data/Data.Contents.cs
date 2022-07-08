using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.Data
{
    #region Stats
    //미리 정의되어있는 csv 나 json의 열들을 써놓는거야
    //이건 세이브라기보다는 이미 정의가 되어있는 리소스를 불러오는 것 과 같아.
    [Serializable]
    public class PlayerStat
    {
        public int level;
        public int maxHp;
        public int str;
        public int dex;
        public int intel;
        public int defense;
        public float accuracy;
        public float dodge;
        public int totalExp;
    }
    [Serializable]
    public class PlayerStatData : ILoaderDict<int, PlayerStat>
    {
        public List<PlayerStat> PlayerStat = new List<PlayerStat>();

        public Dictionary<int, PlayerStat> MakeDict()
        {
            Dictionary<int, PlayerStat> dict = new Dictionary<int, PlayerStat>();
            foreach (PlayerStat stat in PlayerStat)
            {
                //구별되는 key 가 뭔지 생각하고 쓰자
                dict.Add(stat.level, stat);

            }
            return dict;
        }
    }


    [Serializable]
    public class EnemyStat : Data
    {
        public int enemy_id;
        public string name;
        public int maxHp;
        public float attack;
        public float defense;
        public float accuracy;
        public float dodge;

        public float moveSpeed;
        public float detectionRange;
        public float attackDelay;
        public float attackInterval;
        public float attackDuration;
        public float attackRange;
        public float takeDamageDelay;
        public bool immediateAttack;
        public int rewardExp;
    }
    [Serializable]
    public class EnemyStatData : ILoaderDict<int, EnemyStat>
    {
        public List<EnemyStat> EnemyStat = new List<EnemyStat>();

        public Dictionary<int, EnemyStat> MakeDict()
        {
            Dictionary<int, EnemyStat> dict = new Dictionary<int, EnemyStat>();
            foreach(EnemyStat stat in EnemyStat)
            {
                dict.Add(stat.enemy_id, stat);
            }
            return dict;
        }
    }

    #endregion
    [Serializable]
    public class ItemInfo : Data
    {
        public int itemTemplateid;
        public string name;
        public string description;
        public int size;
        public int maxCount;
        public Enums.ItemType itemType;
        
    }
    [Serializable]
    public class ItemInfoData : ILoaderDict<int, ItemInfo>
    {
        public List<ItemInfo> ItemInfo = new List<ItemInfo>();

        public Dictionary<int, ItemInfo> MakeDict()
        {
            Dictionary<int, ItemInfo> dict = new Dictionary<int, ItemInfo>();
            foreach (ItemInfo item in ItemInfo)
            {
                dict.Add(item.itemTemplateid, item);
            }
            return dict;
        }
    }

    [Serializable]
    public class WeaponInfo : ItemInfo
    {
        public Enums.WeaponType weaponType;
        public int attack;

    }
    [Serializable]
    public class WeaponInfoData : ILoaderDict<int, WeaponInfo>
    {
        public List<WeaponInfo> WeaponInfo = new List<WeaponInfo>();
        public Dictionary<int, WeaponInfo> MakeDict()
        {
            Dictionary<int, WeaponInfo> dict = new Dictionary<int, WeaponInfo>();
            foreach (WeaponInfo item in WeaponInfo)
            {
                dict.Add(item.itemTemplateid, item);
            }
            return dict;
        }
    }
    [Serializable]
    public class ArmorInfo : ItemInfo
    {
        public Enums.ArmorType armorType;
        public int defense;

    }
    [Serializable]
    public class ArmorInfoData : ILoaderDict<int, ArmorInfo>
    {
        public List<ArmorInfo> ArmorInfo = new List<ArmorInfo>();
        public Dictionary<int, ArmorInfo> MakeDict()
        {
            Dictionary<int, ArmorInfo> dict = new Dictionary<int, ArmorInfo>();
            foreach (ArmorInfo item in ArmorInfo)
            {
                dict.Add(item.itemTemplateid, item);
            }
            return dict;
        }
    }

    [Serializable]
    public class ConsumableInfo : ItemInfo
    {
        public Enums.ConsumableType consumableType;
        public int hp;
        public Enums.BuffType buffType;

    }
    [Serializable]
    public class ConsumableInfoData : ILoaderDict<int, ConsumableInfo>
    {
        public List<ConsumableInfo> ConsumableInfo = new List<ConsumableInfo>();
        public Dictionary<int, ConsumableInfo> MakeDict()
        {
            Dictionary<int, ConsumableInfo> dict = new Dictionary<int, ConsumableInfo>();
            foreach (ConsumableInfo item in ConsumableInfo)
            {
                dict.Add(item.itemTemplateid, item);
            }
            return dict;
        }
    }


    [Serializable]
    public class UseableInfo : ItemInfo
    {
        public Enums.UseableType useableType;
        

    }
    [Serializable]
    public class UseableInfoData : ILoaderDict<int, UseableInfo>
    {
        public List<UseableInfo> UseableInfo = new List<UseableInfo>();
        public Dictionary<int, UseableInfo> MakeDict()
        {
            Dictionary<int, UseableInfo> dict = new Dictionary<int, UseableInfo>();
            foreach (UseableInfo item in UseableInfo)
            {
                dict.Add(item.itemTemplateid, item);
            }
            return dict;
        }
    }

}