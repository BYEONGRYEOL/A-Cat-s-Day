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
        // �󸶳� �־����� ������ΰ�?
        [SerializeField] protected float stoppingDistance;
        [SerializeField] protected bool autoBraking;

        //priority ������Ʈ���� ���� ȸ������ �����ϴ� �켱���� 0to 99
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

            //���� ���� X �ʱ�ȭ
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
                //������� ����
                // �̵������̳� ���ݹ����� ��� ���� �ڵ�

                //��ġ�� �ڲ� z =0 �� ��������
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);

                instantDirection = target.position - transform.position;
                distance = instantDirection.sqrMagnitude;
                direction = instantDirection.normalized;
                //���¸ӽ��� Update �Լ� ���� 
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
            Debug.Log("IDLE State ����");

            //idle state ���Խ� �ൿ
            agent.ResetPath();
        }
        protected override void Enter_TakeDamage()
        {
            Debug.Log("TakeDamage State ����");

            HPBar_Activate();
            Managers.Time.SetTimer(15, HPBar_DeActivate);
            TakeDamage();
            
        }
        protected override void Enter_Move()
        {
            Debug.Log("Move State ����");

        }
        protected override void Enter_Attack_1()
        {
            Debug.Log("Attack State ����");
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
            //����ؼ� �����ϰ� �����̰ų� ������ �ְų� ���ڸ��� ���ư��ų� 
            //������ �ٸ��� �ؾ��Ѵ�.
            if (distance < stat.DetectionRange)
            {
                State = Enums.State.Move;
            }
        }
        protected override void Update_TakeDamage()
        {
            //���� ��Ȳ���ȿ� ��� ����Ǿ����� �ϴ°�
        }
        protected override void Update_Move()
        {
            //�̵����� ���¿��� ����ؼ� ����Ǿ���ϴ°�
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
            Debug.Log("enemy ���� �õ� ������ ::" + stat.AttackDelay + "��");
            AttackBox_Visible();
            yield return new WaitForSeconds(stat.AttackDelay);
            AttackBox_TriggerOn();
            
            Debug.Log("enemy ���� ���ӽð� ���� ::" + stat.AttackDuration + "��");

            yield return new WaitForSeconds(stat.AttackDuration);
            AttackBox_TriggerOff();

            AttackBox_InVisible();
            Debug.Log("enemy ���� ���� ������ ::" + stat.AttackInterval + "��");

            yield return new WaitForSeconds(stat.AttackInterval);
            StopAttack();
        }
        protected virtual void AttackBox_Visible()
        {
            Debug.Log("���� �÷��̾ ����");
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
            Debug.Log("���� �÷��̾� ���� ����");
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