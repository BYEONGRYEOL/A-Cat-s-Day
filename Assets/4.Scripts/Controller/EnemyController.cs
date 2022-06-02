using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Isometric
{
    public class EnemyController : CharacterController
    {
        public Stat stat;
        protected bool takeDamage = false;
        protected float distance;

        [SerializeField] private GameObject attackBox;
        [SerializeField] private GameObject attackBox_Visible;
        
        

        private Vector3 instantDirection = new Vector3(0, 0, 0);

        protected NavMeshAgent agent;

        protected bool useNMA = true;
        #region NavMesh
        //NavyMesh

        [SerializeField] protected float radius;
        [SerializeField] protected float height;
        [SerializeField] protected float speed;
        [SerializeField] protected float angularSpeed;
        [SerializeField] protected float acceleration;
        // 얼마나 멀어지면 멈출것인가?
        [SerializeField] protected float stoppingDistance;
        [SerializeField] protected bool autoBraking;

        //priority 에이전트끼리 누가 회피할지 결정하는 우선순위 0to 99
        [SerializeField] protected float priority;
        [SerializeField] protected bool autoRepath;

        #endregion

      
        protected float attackRange;
        protected float enemyDamage;
        [SerializeField] GameObject hpbar;
        protected Transform target;
        public Transform Target
        {
            get => target; set => target = value;
        }
        protected override void Start()
        {
            Init();
        }
        protected override void Init()
        {
            Target = FindObjectOfType<PlayerController>().transform;
            myAnimator = GetComponent<Animator>();
            stat = GetComponent<Stat>();
            hpbar.gameObject.SetActive(false);

            //공격 판정 X 초기화
            attackBox.SetActive(false);
            attackBox_Visible.SetActive(false);
            
            attackRoutine = null;

            
            
            
            if(useNMA)
            {
                agent = GetComponent<NavMeshAgent>();

                agent.updateRotation = false;
                agent.updateUpAxis = false;
                agent.speed = speed;
                agent.angularSpeed = angularSpeed;
                agent.radius = radius;
                agent.acceleration = acceleration;
                agent.autoRepath = autoRepath;
            }

            stat.AttackRange = 1f;
            stat.DetectionRange = 25f;
            stat.AttackDelay = 1f;
            
        }
        protected virtual void Update()
        {

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
            switch (state)
            {
                case Enums.State.Idle:
                    Update_Idle();
                    break;
                case Enums.State.Move:
                    Update_Move();
                    break;
                case Enums.State.Attack_1:
                    Update_Attack();
                    break;
                case Enums.State.Run:
                    Update_Run();
                    break;
                case Enums.State.Dodge:
                    Update_Dodge();
                    break;
                case Enums.State.Die:
                    Update_Die();
                    break;
                case Enums.State.TakeDamage:
                    Update_TakeDamage();
                    break;
            }
        }
        protected override void Enter_Idle()
        {
            Debug.Log("IDLE State 진입");

            //idle state 진입시 행동
            agent.ResetPath();
        }
        protected override void Enter_TakeDamage()
        {
            Debug.Log("TakeDamage State 진입");

            HPBar_Activate();
            Managers.Time.SetTimer(15, HPBar_DeActivate);
            TakeDamage();
            
        }
        protected override void Enter_Move()
        {
            Debug.Log("Move State 진입");

        }
        protected override void Enter_Attack_1()
        {
            Debug.Log("Attack State 진입");
        }
        protected override void Enter_Attack_2()
        {

        }
        protected override void Enter_Attack_3()
        {

        }
        protected override void Enter_Run()
        {

        }
        protected override void Enter_Dodge()
        {

        }
        protected override void Enter_Die()
        {

        }
        protected override void Enter_Jump()
        {

        }
        protected override void Update_Idle()
        {
            //계속해서 랜덤하게 움직이거나 가만히 있거나 제자리로 돌아가거나 
            //적마다 다르게 해야한다.
            if (distance < stat.DetectionRange)
            {
                State = Enums.State.Move;
            }
        }
        protected override void Update_TakeDamage()
        {
            //맞은 상황동안에 계속 실행되었으면 하는거
        }
        protected override void Update_Move()
        {
            //이동중인 상태에서 계속해서 실행되어야하는거
            agent.SetDestination(Target.position);
            if(distance < stat.AttackRange)
            {
                State = Enums.State.Attack_1;
            }
            else if(distance > stat.DetectionRange)
            {
                State = Enums.State.Idle;
            }
        }
        protected override void Update_Attack()
        {
            if( distance > stat.AttackRange && attackRoutine == null)
            {
                State = Enums.State.Idle;
            }
            Attack();
        }
        protected override void Update_Run()
        {

        }
        protected override void Update_Dodge()
        {

        }
        protected override void Update_Die()
        {

        }

        private void TakeDamage()
        {
            takeDamageRoutine = StartCoroutine(TakeDamageAnimation());
        }
        private void StopTakeDamage()
        {
            if(takeDamageRoutine!= null)
            {
                StopCoroutine(TakeDamageAnimation());
                takeDamageRoutine = null;
            }
            State = Enums.State.Idle;
        }
        protected virtual IEnumerator TakeDamageAnimation()
        {
            agent.ResetPath();
            yield return new WaitForSeconds(stat.TakeDamageDelay);
            StopTakeDamage();
        }
        private void Attack()
        {
            if(attackRoutine == null)
            {
                attackRoutine = StartCoroutine(AttackAnimation());
            }
        }
        protected virtual IEnumerator AttackAnimation()
        {
            myAnimator.SetBool("isAttacking", true);
            Debug.Log("enemy 공격 시도 딜레이 ::" + stat.AttackDelay + "초");
            AttackBox_Visible();
            yield return new WaitForSeconds(stat.AttackDelay);
            AttackBox_TriggerOn();
            
            Debug.Log("enemy 공격 지속시간 유지 ::" + stat.AttackDuration + "초");

            yield return new WaitForSeconds(stat.AttackDuration);
            AttackBox_TriggerOff();

            AttackBox_InVisible();
            Debug.Log("enemy 공격 이후 딜레이 ::" + stat.AttackInterval + "초");

            yield return new WaitForSeconds(stat.AttackInterval);
            StopAttack();
        }
        protected virtual void AttackBox_Visible()
        {
            Debug.Log("적이 플레이어를 공격");
            attackBox_Visible.transform.localPosition = new Vector3(direction.x * 0.6f, direction.y * 0.6f, 0);
            agent.ResetPath();
            attackBox_Visible.gameObject.SetActive(true);
        }
        protected virtual void AttackBox_TriggerOn()
        {
            Debug.Log("AttackBox_TriggerOn");
            attackBox.SetActive(true);
        }

        protected virtual void AttackBox_TriggerOff()
        {
            attackBox.SetActive(false);
        }
        protected virtual void AttackBox_InVisible()
        {
            attackBox_Visible.gameObject.SetActive(false);
        }
        public void GetAttacked(Stat attacker)
        {
            stat.GetAttacked(attacker);
            Debug.Log("enemy GetAttacked");
            
            State = Enums.State.TakeDamage;
        }
        public void OnHit()
        {
            Debug.Log("적이 플레이어 공격 판정");
            PlayerController.Instance.GetAttacked(stat);
        }

        public void HPBar_Activate()
        {
            hpbar.gameObject.SetActive(true);
        }
        public void HPBar_DeActivate()
        {
            hpbar.gameObject.SetActive(false);
        }
        protected virtual void StopAttack()
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);

                attackRoutine = null;
                myAnimator.SetBool("isAttacking", false);
            }
        }

        protected virtual void FightAction()
        {
            Debug.Log("FightingState");
            Vector3 attackDirection = target.position - transform.position;
            attackDirection.z = 0;
            attackRoutine = StartCoroutine(AttackAnimation());
        }

    }

}