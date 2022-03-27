using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class Player : Character
    {
        // 스피드 조정 변수
        
        
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


        protected override void Start()
        {
            base.Start();
            attackDelay = 0.5f;
            myRigid2D = GetComponent<Rigidbody2D>();
            Time.timeScale = 1f;
            // 컴포넌트 할당
            applySpeed = walkSpeed;
            //Cursor.visible = false;
            target = GameObject.Find("target");

        }

        protected override void Update()
        {
            
            TryJump();
            TryRun();
            TryCrouch();
            //move를 위한 키 입력은 Update 함수에 받기
            TryAttack();
            TryMove();
            
            
            TryInterAction();
            base.Update();
        }
        private void FixedUpdate()
        {
            //실제 움직임은 FixedUpdate에
            if (!isAttacking)
            {
                myRigid2D.MovePosition(transform.position + velocity * Time.deltaTime);

            }
        }

        // 점프 시도
        private void TryJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && isGround)
            {
                Jump();
            }
        }
        private void TryInterAction()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                InterAction();
            }
        }
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
        private void TryRun()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Running();
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                RunningCancel();
            }
        }

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

        // 앉기 시도
        private void TryCrouch()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Crouch();
            }
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


        private void TryMove()
        {
           
            originalDirection.x = Input.GetAxisRaw("Horizontal");
            originalDirection.y = Input.GetAxisRaw("Vertical");
            originalDirection.z = 0;

            direction = originalDirection.normalized;
            velocity = direction * applySpeed;
        }

        private void TryAttack()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!isAttacking)
                {
                    attackRoutine = StartCoroutine(AttackAnimation());
                }
            }
        }
        protected override void StopAttack()
        {
            base.StopAttack();
            attackBox_Tran.gameObject.SetActive(false);
        }
        protected override void Attack()
        {
            Debug.Log("공격");
            // 자식 오브젝트의 위치 = localPosition으로 하자
            attackBox_Tran.localPosition = new Vector3(originalDirection.x * 0.5f, originalDirection.y * 0.5f, 0);
            
            if(originalDirection.x + originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0,0,45);
            }
            else if(originalDirection.x + originalDirection.y == -2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if(originalDirection.x - originalDirection.y == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if(originalDirection.y - originalDirection.x == 2)
            {
                attackBox_Tran.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if(originalDirection.x - originalDirection.y == 1)
            {
                if(originalDirection.x > 0)
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
            else if(originalDirection.x - originalDirection.y == -1)
            {
                if(originalDirection.y > 0)
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
            
            attackBox_Tran.gameObject.SetActive(true);
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