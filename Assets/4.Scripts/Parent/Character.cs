using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField]

        protected Vector3 originalDirection;
        protected Vector3 direction;

        protected float attackDelay;
        protected bool isAttacking = false;
        protected bool canMove = true;
        protected bool takeDamage = false;
        private Animator myAnimator;
        protected Coroutine attackRoutine;


        public enum LayerName
        {
            IdleLayer = 0,
            WalkLayer = 1,
            AttackLayer = 2,
            HitDamageLayer = 3
        }

        public bool IsMoving
        {
            get
            {
                return direction.x != 0 || direction.y != 0;
            }
        }


        protected virtual void Start()
        {
            myAnimator = GetComponent<Animator>();
        }


        protected virtual void Update()
        {
            HandleLayers();
        }

        


        public void HandleLayers()
        {
            if (IsMoving)
            {
                ActivateLayer(LayerName.WalkLayer);
                myAnimator.SetFloat("x", direction.x);
                myAnimator.SetFloat("y", direction.y);

                //StopAttack();
            }
            else if(isAttacking)
            {
                ActivateLayer(LayerName.AttackLayer);
            }
            else if (takeDamage)
            {
                ActivateLayer(LayerName.HitDamageLayer);
            }
            else
            {
                ActivateLayer(LayerName.IdleLayer);
            }
        }

        protected virtual IEnumerator AttackAnimation()
        {
            isAttacking = true;
            canMove = false;
            myAnimator.SetBool("isAttacking", isAttacking);
            
            Attack();
            yield return new WaitForSeconds(attackDelay);
            StopAttack();
        }
        protected virtual void Attack()
        {

        }

        public virtual void TakeDamage()
        {
            
        }
        protected virtual void StopAttack()
        {
            if(attackRoutine != null)
            {
                StopCoroutine(attackRoutine);
                isAttacking = false;
                canMove = true;
                myAnimator.SetBool("isAttacking", isAttacking);
            }
        }

        public void ActivateLayer(LayerName layerName)
        {
            // 모든 레이어의 무게값을 0 으로 만들어 줍니다.
            for (int i = 0; i < myAnimator.layerCount; i++)
            {
                myAnimator.SetLayerWeight(1, 0);
            }

            myAnimator.SetLayerWeight((int)layerName, 1);
        }
    }

}