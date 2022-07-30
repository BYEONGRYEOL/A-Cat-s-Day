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
        

        //���� ����

        
        
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
            //�׾����� ��������
            if (State != Enums.State.Die)
            {
                //������� ����
                // �̵������̳� ���ݹ����� ��� ���� �ڵ�

                //��ġ�� �ڲ� z =0 �� ��������
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                
                instantDirection = target.position - transform.position;
                distance = instantDirection.sqrMagnitude;
                direction = instantDirection.normalized;
                //���¸ӽ��� Update �Լ� ���� 
               

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
            Debug.Log("���� �÷��̾ ����");
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