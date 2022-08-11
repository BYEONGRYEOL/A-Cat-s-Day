using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    #region Inspector
    public Vector3 originalDirection;
    public Vector3 direction;
    protected Animator myAnimator;
    protected Coroutine attackRoutine;
    protected Coroutine takeDamageRoutine;
    #endregion

    protected virtual void Start()
    {
        Init();
    }

    protected Enums.State state = Enums.State.Idle;

    public Enums.State State
    {
        get => state;
        set
        {
            state = value;
            myAnimator = GetComponent<Animator>();

            switch (state)
            {
                //공용
                case Enums.State.Idle:
                    ActivateAnimationLayer(Enums.AnimationLayer.IdleLayer);
                    myAnimator.SetBool("isAttacking", false);
                    Enter_Idle();
                    break;

                case Enums.State.Move:
                    ActivateAnimationLayer(Enums.AnimationLayer.WalkLayer);
                    myAnimator.SetBool("isAttacking", false);
                    Enter_Move();
                    break;

                case Enums.State.Attack_1:
                    ActivateAnimationLayer(Enums.AnimationLayer.AttackLayer);
                    myAnimator.SetBool("isAttacking", true);
                    myAnimator.SetInteger("attackCount", 1);
                    Enter_Attack_1();
                    break;
                case Enums.State.TakeDamage:
                    ActivateAnimationLayer(Enums.AnimationLayer.HitDamageLayer);
                    Enter_TakeDamage();
                    break;

                //우선은 플레이어 전용

                case Enums.State.Attack_2:
                    myAnimator.SetInteger("attackCount", 2);
                    Enter_Attack_2();
                    break;

                case Enums.State.Attack_3:
                    myAnimator.SetInteger("attackCount", 3);
                    Enter_Attack_3();
                    break;

                case Enums.State.Attack_Jump:
                    Enter_Jump();
                    break;

                case Enums.State.Attack_Run:
                    Enter_Run();
                    break;

            }
        }
    }
    protected virtual void Init()
    {
        myAnimator = GetComponent<Animator>();
    }

    protected virtual void Update_Idle()
    {

    }
    protected virtual void Update_TakeDamage()
    {
    }
    protected virtual void Update_Move()
    {
        
    }
    protected virtual void Update_Attack()
    {
        
    }
    protected virtual void Update_Run()
    {

    }
    protected virtual void Update_Dodge()
    {

    }
    protected virtual void Update_Die()
    {

    }

    protected virtual void Enter_Idle()
    {

    }
    protected virtual void Enter_TakeDamage()
    {
    }
    protected virtual void Enter_Move()
    {

    }
    protected virtual void Enter_Attack_1()
    {

    }
    protected virtual void Enter_Attack_2()
    {

    }
    protected virtual void Enter_Attack_3()
    {

    }
    protected virtual void Enter_Run()
    {

    }
    protected virtual void Enter_Dodge()
    {

    }
    protected virtual void Enter_Die()
    {

    }
    protected virtual void Enter_Jump()
    {

    }
    public void ActivateAnimationLayer(Enums.AnimationLayer layerName)
    {
        // 모든 레이어의 무게값을 0 으로 만들어 줍니다.
        for (int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }
        myAnimator.SetLayerWeight((int)layerName, 1);
    }

}
