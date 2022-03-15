using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Isometric
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAI : Character
    {
        Player_HP player_hp;
        GameObject player;
        NavMeshAgent agent;
        private int enemyState = 0;
        private bool isAttacking = false;
        float distance;
        private Vector3 instantDirection = new Vector3(0, 0, 0);
        //NavyMesh
        [SerializeField] private float radius;
        [SerializeField] private float height;
        [SerializeField] private float speed;
        [SerializeField] private float angularSpeed;
        [SerializeField] private float acceleration;
        // 얼마나 멀어지면 멈출것인가?
        [SerializeField] private float stoppingDistance;
        [SerializeField] private bool autoBraking;
        
        //priority 에이전트끼리 누가 회피할지 결정하는 우선순위 0to 99
        [SerializeField] private float priority;
        [SerializeField] bool autoRepath;

        //적의 성질

        public float detectionRange;
        public float attackRange;
        public float attackDelay;
        public float enemyDamage;

        protected override void Start()
        {
            base.Start();
            player = GameObject.FindGameObjectWithTag("Player");
            player_hp = player.GetComponent<Player_HP>();
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


        }
        protected override void Update()
        {
            
            
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            instantDirection = player.transform.position - transform.position;
            Debug.Log("Update Debug :: instantDirection" + instantDirection + "direction" + direction + "distance" + distance);

            distance = instantDirection.sqrMagnitude;
            direction = instantDirection.normalized;
            Check_Player_Location();
            base.Update();
        }

        public void Check_Player_Location()
        {
            if (distance < attackRange && isAttacking == false)
            {
                FightingState();
            }
            else if (distance < detectionRange && isAttacking == false)
            {
                ChaseState();
            }
            else if (distance >= detectionRange)
            {
                IdleState();
            }
        }
        public void IdleState()
        {
            Debug.Log("IdleState");
            agent.ResetPath();
        }
        public void ChaseState()
        {
            Debug.Log("ChaseState");
            agent.SetDestination(player.transform.position);

        }
        public void FightingState()
        {
            Debug.Log("FightingState");
            instantDirection = player.transform.position - transform.position;
            instantDirection.z = 0;
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            isAttacking = true;
            yield return new WaitForSeconds(attackDelay);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, instantDirection, attackRange * 10, LayerMask.GetMask("Player"));
            if (hit.collider.CompareTag("Player"))
            {
                player_hp.HP_Changed(enemyDamage);
            }
            Debug.Log("attack");
            Debug.Log("dir.x = " + instantDirection.x + "dir.y = " + instantDirection.y + "dir.z = " + instantDirection.z);
            isAttacking = false;
        }
    }

}