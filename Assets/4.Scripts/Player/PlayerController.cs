using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Data;
using Isometric.UI;
using UnityEngine.EventSystems;
using Isometric.Utility;
namespace Isometric
{

    public class PlayerController : Character
    {
        // 스피드 조정 변수
        private static PlayerController instance;

        public static PlayerController Instance { get { return instance; } }
        
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float runSpeed;
        [SerializeField]
        private float crouchSpeed;
        private float applySpeed;

        // 점프 정도
        [SerializeField]
        private float jumpForce;

        // 상태 변수
        
        private bool isRun = false;
        private bool isGround = true;
        private bool isCrouch = false;
        private bool isLying = false;
        private Vector3 player_sight = new Vector3(0, 0, 0);
        private Vector3 velocity = new Vector3(0, 0, 0);
        // 키 입력 감지 변수
        private bool moveKeyinput = false;
        private bool runKeyinput = false;
        // 앉았을 때 얼마나 앉을지 결정하는 변수
        [SerializeField]
        private float crouchPosY;
        private float applyCrouchPosY;
        //누울 때 회전

        // 필요한 컴포넌트
        [SerializeField]
        private Camera theCamera;
        private Rigidbody2D myRigid2D;
        
        [SerializeField] private Transform attackBox_Tran;
        GameObject target;
        private Player_HP player_hp;

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
        protected override void Start()
        {
            base.Start();

            Managers.Input.KeyAction -= OnKeyboard;
            Managers.Input.KeyAction += OnKeyboard;

            Managers.Input.MouseAction -= OnMouse;
            Managers.Input.MouseAction += OnMouse;

            //Debug.Log(Managers.KeyBind.KeyBinds["INTERACTION"]);

            attackDelay = 0.5f;
            attackDuration = 0.1f;

            player_hp = GetComponent<Player_HP>();
            myRigid2D = GetComponent<Rigidbody2D>();
            Time.timeScale = 1f;
            // 컴포넌트 할당
            applySpeed = walkSpeed;
            //Cursor.visible = false;
            target = GameObject.Find("target");

        }

        protected override void Update()
        {

            if (!GameManager.Instance.IsPaused)
            {
                
                base.Update(); 
            }
        }
        private void FixedUpdate()
        {
            //실제 움직임은 FixedUpdate에
            if (!isAttacking)
            {
                

                myRigid2D.MovePosition(transform.position + velocity * Time.deltaTime);
            }
            AttackBox_Location_Set();
        }
        private void OnKeyboard()
        {
            Debug.Log("OnKeyBoard 실행중");
            originalDirection.z = 0;

            //점프
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                Jump();
            }
            //상호작용
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["INTERACTION"]))
            {
                InterAction();
            }
            //뛰기
            if (Input.GetKey(Managers.KeyBind.KeyBinds["RUN"]))
            {
                Running();
            }
            if (Input.GetKeyUp(Managers.KeyBind.KeyBinds["RUN"]))
            {
                RunningCancel();
            }
            //웅크리기
            if (Input.GetKeyDown(Managers.KeyBind.KeyBinds["CROUCH"]))
            {
                Crouch();
            }


            if(Input.GetKey(Managers.KeyBind.KeyBinds["UP"]))
            {
                originalDirection.y = 1;
            }
            else if (Input.GetKey(Managers.KeyBind.KeyBinds["DOWN"]))
            {
                originalDirection.y = -1;
            }
            else
            {
                originalDirection.y = 0;
            }
            if (Input.GetKey(Managers.KeyBind.KeyBinds["RIGHT"]))
            {
                originalDirection.x = 1;
            }
            else if (Input.GetKey(Managers.KeyBind.KeyBinds["LEFT"]))
            {
                originalDirection.x = -1;
            }
            else
            {
                originalDirection.x = 0;
            }


            //이동


            direction = originalDirection.normalized;
            velocity = direction * applySpeed;

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

            //공격
            //마우스 클릭을 인식하는 경우 항상 UI elements 클릭시와 구별할 수 있도록 코드 추가
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if (!isAttacking)
                {
                    attackRoutine = StartCoroutine(AttackAnimation());
                }
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
            if (isCrouch)
                Crouch();

            myRigid2D.velocity = transform.up * jumpForce;
        }

        // 달리기 시도
        
        // 달리기
        private void Running()
        {
            if (isCrouch)
                Crouch();

            isRun = true;
            applySpeed = runSpeed;
        }

        // 달리기 취소
        private void RunningCancel()
        {
            isRun = false;
            applySpeed = walkSpeed;
        }

       

        // 앉기 동작
        private void Crouch()
        {
            isCrouch = !isCrouch;
            if (isCrouch)
            {
                applySpeed = crouchSpeed;
                applyCrouchPosY = crouchPosY;
            }
            else
            {
                applySpeed = walkSpeed;

            }

        }

        // 부드러운 앉기 동작


        protected override void StopAttack()
        {
            base.StopAttack();
        }
        protected override void AttackBox_Disable()
        {
            attackBox_Tran.gameObject.SetActive(false);
        }
        protected override void Attack()
        {
            Debug.Log("공격");
            attackBox_Tran.gameObject.SetActive(true);
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