using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;
using Isometric.UI;
using UnityEngine.EventSystems;
using Isometric.Utility;
namespace Isometric
{
    public class PlayerController : MonoBehaviour
    {
        // 스피드 조정 변수
        private static PlayerController instance;
        public static PlayerController Instance { get { return instance; } }
        
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float runSpeed;

        // 점프 정도
        [SerializeField]
        private float jumpForce;
        
        private Vector3 player_sight = new Vector3(0, 0, 0);
        private Vector3 velocity = new Vector3(0, 0, 0);

        // 키 입력 감지 변수
        private bool moveKeyinput = false;
        private bool runKeyinput = false;

        // 앉았을 때 얼마나 앉을지 결정하는 변수
        [SerializeField]
        private float crouchPosY;
        private float applyCrouchPosY;
        
        // 필요한 컴포넌트
        [SerializeField]
        private Camera theCamera;
        private Rigidbody2D myRigid2D;
        
        [SerializeField] private Transform attackBox_Tran;

        GameObject target;
        private Player_HP player_hp;

        protected Vector3 originalDirection;
        protected Vector3 direction;

        protected float attackDelay;
        protected float attackDuration;

        protected bool isAttacking = false;
        protected bool canMove = true;
        
        private Animator myAnimator;
        protected Coroutine attackRoutine;

        public State state = State.Idle;


        public enum State
        {
            Die,
            Idle,
            Move,
            Run,
            Attack_1,
            Attack_2,
            Attack_3,
            Dodge,
            Interaction,
            Climbing,
            TakeDamage,
            Crouch
        }

        public enum AnimationLayer
        {
            IdleLayer = 0,
            WalkLayer = 1,
            AttackLayer = 2,
            HitDamageLayer = 3
        }

        private IEnumerator AttackAnimation()
        {
            state = State.Attack_1;

            myAnimator.SetBool("isAttacking", true);

            Attack_Activate();
            yield return new WaitForSeconds(attackDuration);

            AttackBox_Disable();
            yield return new WaitForSeconds(attackDelay);
            StopAttack();
        }

        public void ActivateLayer(AnimationLayer layerName)
        {
            // 모든 레이어의 무게값을 0 으로 만들어 줍니다.
            for (int i = 0; i < myAnimator.layerCount; i++)
            {
                myAnimator.SetLayerWeight(i, 0);
            }
            myAnimator.SetLayerWeight((int)layerName, 1);
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }
        }
        void Start()
        {
            myAnimator = GetComponent<Animator>();

            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;

            //콜백처리
            //player state 안에서 처리할수 없어서 OnMouse에서 직접 조건걸기
            Managers.Input.MouseAction -= OnMouse;
            Managers.Input.MouseAction += OnMouse;

            //Debug.Log(Managers.KeyBind.KeyBinds["INTERACTION"]);

            attackDelay = 0.5f;
            attackDuration = 0.1f;

            player_hp = GetComponent<Player_HP>();
            myRigid2D = GetComponent<Rigidbody2D>();
            Time.timeScale = 1f;
            
            //Cursor.visible = false;
            target = GameObject.Find("target");
        }
        void Update()
        {
            switch(state) 
            {
                case State.Idle:
                    Update_Idle();
                    break;
                case State.Move:
                    Update_Move();
                    break;
                case State.Attack_1:
                    Update_Attack();
                    break;
                case State.Run :
                    Update_Run();
                    break;
                case State.Interaction:
                    Update_InterAction();
                    break;
                case State.Dodge:
                    Update_Dodge();
                    break;
                case State.Die:
                    Update_Die();
                    break;
                case State.Climbing:
                    Update_Climbing();
                    break;
                case State.TakeDamage:
                    Update_TakeDamage();
                    break;
            }
        }
        void Update_TakeDamage()
        {
            ActivateLayer(AnimationLayer.HitDamageLayer);
        }
        void Update_Crouch()
        {

        }
        void Update_Idle()
        {
            ActivateLayer(AnimationLayer.IdleLayer);

            if(originalDirection.sqrMagnitude > 0)
            {
                state = State.Move;
            }
        }
        void Update_Move()
        {
            ActivateLayer(AnimationLayer.WalkLayer);
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            myMove(walkSpeed);

            if (direction.magnitude == 0)
            {
                state = State.Idle;
            }
        }
        void Update_Attack()
        {
            Debug.Log("실행?");
            ActivateLayer(AnimationLayer.AttackLayer);
        }
        void Update_Run()
        {
            myMove(runSpeed);

            if (originalDirection.magnitude == 0)
            {
                state = State.Idle;
            }
        }
        void Update_InterAction()
        {

        }
        void Update_Dodge()
        {

        }
        void Update_Die()
        {

        }
        void Update_Climbing()
        {

        }
        private void FixedUpdate()
        {
            AttackBox_Location_Set();
        }

        private void myMove(float speed)
        {
            
            direction = originalDirection.normalized;
            velocity = direction * speed;
            myRigid2D.MovePosition(transform.position + velocity * Time.deltaTime);
        }
        
        private void OnKeyboard()
        {
            originalDirection.z = 0;
            //점프
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["JUMP"]))
                Jump();
            
            //상호작용
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["INTERACTION"]))
                InterAction();
            
            //뛰기
            if (Input.GetKey(Managers.KeyBind.KeyBinds["RUN"]))
                Running();
            
            if (Input.GetKeyUp(Managers.KeyBind.KeyBinds["RUN"]))
                RunningCancel();
            //웅크리기
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["CROUCH"]))
                Crouch();

            
            

            if (Input.GetKey(Managers.KeyBind.KeyBinds["UP"]))
                originalDirection.y = 1;

            else if (Input.GetKey(Managers.KeyBind.KeyBinds["DOWN"]))
                originalDirection.y = -1;
            else
                originalDirection.y = 0;



            if (Input.GetKey(Managers.KeyBind.KeyBinds["RIGHT"]))
                originalDirection.x = 1;

            else if (Input.GetKey(Managers.KeyBind.KeyBinds["LEFT"]))
                originalDirection.x = -1;
            else
                originalDirection.x = 0;



            foreach (string action in Managers.KeyBind.ActionBinds.Keys)
            {
                if (Input.GetKeyDown(Managers.KeyBind.ActionBinds[action]))
                {
                    UI_Ingame_R.Instance.ClickActionButton(action);
                    Debug.Log(action + "키입력받음");
                }
            }
        }

        
        private void OnMouse()
        {
            if(state == State.Die)
                return;
            //공격
            //마우스 클릭을 인식하는 경우 항상 UI elements 클릭시와 구별할 수 있도록 코드 추가
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                Debug.Log("다중실행?");
            }

        }
        private void Attack_Activate()
        {
            attackBox_Tran.gameObject.SetActive(true);
        }
        private void Attack()
        {
            //공격 불가상태가 언제지?
            //crouch, 

            //달리면서 공격하면 다르게

            // 점프 공격은 다르게 

            // 첫 공격
            if(state != State.Attack_1 && state != State.Attack_2 && state != State.Attack_3)
            {
                Debug.Log("1차공격");
                attackRoutine = StartCoroutine(AttackAnimation());
                state = State.Attack_1;
            }
            
        }
        private void AttackBox_Location_Set()
        {
            // 자식 오브젝트의 위치 = localPosition으로 하자
            attackBox_Tran.localPosition = new Vector3(originalDirection.x * 0.5f, originalDirection.y * 0.5f, 0);

            if (originalDirection.x + originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x + originalDirection.y == -2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (originalDirection.x - originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.y - originalDirection.x == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (originalDirection.x - originalDirection.y == 1)
            {
                if (originalDirection.x > 0)
                {
                    //우
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 0);

                }
                else
                {
                    //하
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 90);

                }
            }
            else if (originalDirection.x - originalDirection.y == -1)
            {
                if (originalDirection.y > 0)
                {
                    //상
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 90);

                }
                else
                {
                    //좌
                    attackBox_Tran.rotation = Quaternion.Euler(0, 0, 0);

                }
            }
        }
        // 점프 시도
       
        private void InterAction()
        {
            // 이동, 뛰기, 기본, 상태일때만 되는거야
            if (state != State.Idle && state != State.Move && state!= State.Run)
                return;

            state = State.Interaction;

            Debug.DrawRay(transform.position, player_sight * 10, Color.white);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, player_sight, 3f, LayerMask.GetMask("object"));
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.CompareTag("grass"));
            }
            Debug.Log("interaction");
            Debug.Log(player_sight.x + player_sight.y);
        }
        // 점프
        private void Jump()
        {
            myRigid2D.velocity = transform.up * jumpForce;
        }

        // 달리기 시도
        
        // 달리기
        private void Running()
        {
            //idle, Move, 
            if (state != State.Idle && state != State.Move)
                return;

            state = State.Run;
        }

        // 달리기 취소
        private void RunningCancel()
        {
            if (state != State.Run)
                return;
            state = State.Move;
        }

        private void Dodge()
        {
            //맞은 상태, 공중, 에서는 안되어야해
            if (state == State.TakeDamage ||state == State.Die)
                return;
            state = State.Dodge;
        }
       

        // 웅크리기 동작
        private void Crouch()
        {
            if (state != State.Idle)
                return;
            //시간 보내기
        }

        private void StopAttack()
        {
            if (attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                
                
                myAnimator.SetBool("isAttacking", false);
                state = State.Idle;
            }
        }

        public void AttackBox_Disable()
        {
            attackBox_Tran.gameObject.SetActive(false);
        }
        
        public void CastSpell(int spellIndex)
        {
            
        }

        public void Sleep()
        {
            myRigid2D.isKinematic = true;
            /*gameObject.transform.rotation = new[;]*/
            /*gameObject.transform.position = bed.transform.position + new Vector3(1,1);*/
            Debug.Log(transform.rotation);
            Debug.Log(target.transform.rotation);
            //StartCoroutine(SleepCoroutine());

        }
        /*IEnumerator SleepCoroutine()
        {
            *//*Vector3 objPosition = bed.transform.position + new Vector3(1, 1, 0);
            Vector3 nowPosition = gameObject.transform.position;
            Quaternion lyingQuaternion;
            lyingQuaternion = Quaternion.identity;
            lyingQuaternion.eulerAngles = lyingRotation;

            int i = 0;
            while (i < 101)
            {

                gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, lyingQuaternion, lyingTime * Time.deltaTime);
                gameObject.transform.position += (objPosition - nowPosition) / 100;

                i += 1;
                yield return new WaitForSeconds(0.01f);
            }
            isLyingDown = false;
            isLying = true;*//*
        }*/

        public void WakeUp()
        {
            /*isLyingDown = true;
            theCamera.transform.localEulerAngles = new Vector3(0, 0, 0);

            bed = GameObject.FindGameObjectWithTag("Bed").GetComponent<AnimationController>();
            StartCoroutine(WakeUpCoroutine());*/
        }
        /*IEnumerator WakeUpCoroutine()
        {
           *//* Vector3 nowPosition = transform.position;
            wakeUpPosition = bed.transform.position + new Vector3(0, 0.5f, -3);
            Quaternion wakeUpQuaternion;
            wakeUpQuaternion = Quaternion.identity;
            wakeUpQuaternion.eulerAngles = wakeUpRotation;

            int i = 0;
            while (i < 100)
            {
                gameObject.transform.rotation = Quaternion.Lerp(transform.rotation, wakeUpQuaternion, lyingTime * Time.deltaTime);
                gameObject.transform.position += (wakeUpPosition - nowPosition) / 100;
                i += 1;
                yield return new WaitForSeconds(0.01f);
            }
            isLyingDown = false;
            isLying = false;
            myRigid.isKinematic = false;
    *//*
        }*/
    } 
}