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
                        Debug.Log("PlayerStatDict���� �� �������� ����");
                        break;
                    }
                    else if(totalExp < playerStat.totalExp)
                    {
                        //���� �� ����ġ�� ���� ������ ������ ���� ���Ѱ��
                        break;
                    }
                    //
                    nowlevel++;
                }
                if(nowlevel > Level)
                {
                    Debug.Log("������");
                    Level = nowlevel;
                    //�������� ���� ����
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
        
        //���� hp
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
            //���̺� �����Ͱ� ������
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
            // ������ �� ���� ü���� �ִ�ü������ �ʱ�ȭ
            Hp = MaxHp;
            Accuracy = playerStat.accuracy;
            Defense = playerStat.defense;
            Dodge = playerStat.dodge;
            

        }
    }
}