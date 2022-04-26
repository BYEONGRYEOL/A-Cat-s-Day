using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class IdleState : IState
    {
        private Enemy_1 parent;
        public void Enter(Enemy_1 parent)
        {
            Debug.Log(parent.Target.position);
            Debug.Log("Enter IdleState");
            this.parent = parent;
        }
        public void Exit()
        {
            //idlestate에서 나갈 때
        }
        public void Update()
        {
            if (parent.Target != null)
            {
                parent.IdleAction();

                if (parent.DetermineState() == 1)
                {
                    parent.ChangeState(new ChaseState());
                }
                else if(parent.DetermineState() == 2)
                {
                    parent.ChangeState(new FightState());
                }
            }
            else
            {
                parent.ChangeState(new NonState());
            }
        }
    }

}