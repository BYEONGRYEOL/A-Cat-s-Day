using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public abstract class Character : MonoBehaviour
    {

        [SerializeField]
        
        protected Vector3 direction;

        private Animator myAnimator;
        


        public enum LayerName
        {
            IdleLayer = 0,
            WalkLayer = 1
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
            }
            else
            {
                ActivateLayer(LayerName.IdleLayer);
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