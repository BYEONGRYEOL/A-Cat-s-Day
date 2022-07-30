using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Isometric
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy_1 : EnemyController
    {
        
        
        private NavMeshAgent agent;


        private int enemyState = 0;
        
        float distance;

        [SerializeField] private Transform attackBox_Tran;
        private Vector3 instantDirection = new Vector3(0, 0, 0);
        //NavyMesh
        

        //적의 성질

        
        
        public NavMeshAgent Agent
        {
            get => agent;
        }
        
        public Transform Target 
        {
            get => target; set => target = value;
        }

        //
        
        protected void Awake()
        {
            Debug.Log("Enemy Awake");
           
        }
        
        protected override void Update()
        {
            base.Update();
            //죽어있지 않을때만
            if (State != Enums.State.Die)
            {
                //살아있을 때만
                // 이동방향이나 공격방향을 얻기 위한 코드

                //위치가 자꾸 z =0 를 벗어나더라고
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                
                instantDirection = target.position - transform.position;
                distance = instantDirection.sqrMagnitude;
                direction = instantDirection.normalized;
                //상태머신의 Update 함수 구현 
               

            }
        }

        public int DetermineState()
        {
            if(distance < stat.AttackRange)
            {
                return 2;
            }
            if(distance < stat.DetectionRange)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public void IdleAction()
        {
            agent.ResetPath();
        }
        public void ChaseAction()
        {
            agent.SetDestination(target.position);
        }
        

        protected void Attack()
        {
            Debug.Log("적이 플레이어를 공격");
            attackBox_Tran.localPosition = new Vector3(direction.x * 0.6f, direction.y * 0.6f, 0);
            agent.ResetPath();
            attackBox_Tran.gameObject.SetActive(true);
        }
        protected override void AttackBox_InVisible()
        {
            attackBox_Tran.gameObject.SetActive(false);
        }
        
    }

}