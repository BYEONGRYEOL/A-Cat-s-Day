using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class NonState : IState
    {
        private Enemy_1 parent;
        public void Enter(Enemy_1 parent)
        {
            Debug.Log("Enter NonState");

            this.parent = parent;
        }
        public void Update()
        {
            if(parent.Target != null)
            {
                parent.ChangeState(new IdleState());
            }
        }
        public void Exit()
        {
            
        }
    }

}