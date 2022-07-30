using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;

namespace Isometric
{
    public class PlayerStat : Stat
    {
        int level;
        int str;
        int dex;
        int intel;
        int totalExp;
        
        
        public int TotalExp 
        {
            get => totalExp; 
            
            set
            {
                totalExp = value;
                int nowlevel = Level;
                while (true)
                {
                    Data.PlayerStatData playerStat;
                    if(Managers.Data.PlayerStatDict.TryGetValue(nowlevel + 1, out playerStat) == false)
                    {
                        Debug.Log("PlayerStatDict에서 값 가져오기 실패");
                        break;
                    }
                    else if(totalExp < playerStat.totalExp)
                    {
                        //현재 총 경험치가 다음 레벨업 기준을 넘지 못한경우
                        break;
                    }
                    //
                    nowlevel++;
                }
                if(nowlevel > Level)
                {
                    Debug.Log("레벨업");
                    Level = nowlevel;
                    //레벨업시 스탯 갱신
                }
                
            }
        }
        /*//int enemy_id;
        int maxHp;
        float attack;
        float defense;
        float moveSpeed;
        float accuracy;
        float dodge;
        float detectionRange;
        float attackDelay;
        float attackInterval;
        float attackDuration;
        float attackRange;
        int rewardExp;*/
        
        //현재 hp
        //float hp


        
        
        public int Level { get => level; set => level = value; }
        public int Str { get => str; set => str = value; }
        public int Dex { get => dex; set => dex = value; }
        public int Intel { get => intel; set => intel = value; }


        public override void Start()
        {
            base.Start();
            Init();
        }
        public void Init()
        {
            //세이브 데이터가 없으면
            Level = 1;
            SetStatwithLevel(Level);

            MoveSpeed = 2f;
            
            
        }

        public void SetStatwithLevel(int level)
        {

            Dictionary<int, Isometric.Data.PlayerStatData> dict = Managers.Data.PlayerStatDict;
            Data.PlayerStatData playerStat = dict[level];
            str = playerStat.str;
            dex = playerStat.dex;
            intel = playerStat.intel;
            MaxHp = playerStat.maxHp;
            // 레벨업 시 현재 체력을 최대체력으로 초기화
            Hp = MaxHp;
            Accuracy = playerStat.accuracy;
            Defense = playerStat.defense;
            Dodge = playerStat.dodge;
            

        }
    }
}