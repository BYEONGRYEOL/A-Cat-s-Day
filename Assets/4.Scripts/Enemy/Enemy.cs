using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Isometric
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Character
    {
        [SerializeField] private Transform target;
        [SerializeField] private Enemy_HP enemy_hp;

        private NavMeshAgent agent;

        private IState currentState;

        private int enemyState = 0;
        
        float distance;

        [SerializeField] private Transform attackBox_Tran;
        private Vector3 instantDirection = new Vector3(0, 0, 0);
        //NavyMesh
        [SerializeField] private float radius;
        [SerializeField] private float height;
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float acceleration;
        // �󸶳� �־����� ������ΰ�?
        [SerializeField] private float stoppingDistance;
        [SerializeField] private bool autoBraking;
        
        //priority ������Ʈ���� ���� ȸ������ �����ϴ� �켱���� 0to 99
        [SerializeField] private float priority;
        [SerializeField] bool autoRepath;

        //���� ����

        private float detectionRange;
        private float attackRange;
        public float enemyDamage;
        
        
        public float AttackDelay { get => attackDelay; set => attackDelay = value; }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public NavMeshAgent Agent
        {
            get => agent;
        }
        public bool IsAttacking
        {
            get => isAttacking; set => isAttacking = value;
        }
        public Transform Target 
        {
            get => target; set => target = value;
        }

        public void ChangeState(IState newState)
        {
            if(currentState != null)
            {
                currentState.Exit();
            }
            currentState = newState;
            currentState.Enter(this);
        }
        protected void Awake()
        {
            Debug.Log("Enemy Awake");
            ChangeState(new NonState());
        }
        protected override void Start()
        {
            base.Start();

            enemy_hp = GetComponent<Enemy_HP>();

            agent = GetComponent<NavMeshAgent>();

            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speed;
            agent.angularSpeed = angularSpeed;
            agent.radius = radius;
            agent.acceleration = acceleration;
            agent.autoRepath = autoRepath;

            
            attackRange = 1f;
            detectionRange = 25f;
            attackDelay = 1f;
            attackDuration = 0.1f;

        }
        protected override void Update()
        {
            base.Update();
            if (enemy_hp.IsAlive)
            {
                //������� ����
                // �̵������̳� ���ݹ����� ��� ���� �ڵ�
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                instantDirection = target.position - transform.position;
                distance = instantDirection.sqrMagnitude;
                direction = instantDirection.normalized;
                //���¸ӽ��� Update �Լ� ���� 
                currentState.Update();

            }
        }

        public int DetermineState()
        {
            if(distance < attackRange)
            {
                return 2;
            }
            if(distance < detectionRange)
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
        public void FightAction()
        {
            Debug.Log("FightingState");
            instantDirection = target.position - transform.position;
            instantDirection.z = 0;
            attackRoutine = StartCoroutine(AttackAnimation());
        }

        protected override void Attack()
        {
            Debug.Log("���� �÷��̾ ����");
            attackBox_Tran.localPosition = new Vector3(direction.x * 0.6f, direction.y * 0.6f, 0);
            agent.ResetPath();
            attackBox_Tran.gameObject.SetActive(true);
        }
        protected override void AttackBox_Disable()
        {
            attackBox_Tran.gameObject.SetActive(false);
        }
        protected override void StopAttack()
        {
            base.StopAttack();
        }
    }

}