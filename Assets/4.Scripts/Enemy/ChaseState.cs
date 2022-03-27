using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class ChaseState : IState
    {
        private Enemy parent;
        // Start is called before the first frame update
        public void Enter(Enemy parent)
        {
            Debug.Log("Enter ChaseState");

            this.parent = parent;
        }

        // Update is called once per frame
        public void Update()
        {
            if(parent.Target != null)
            {
                parent.ChaseAction();

                if(parent.DetermineState() == 2)
                {
                    parent.ChangeState(new FightState());
                }
                else if(parent.DetermineState() == 0)
                {
                    parent.ChangeState(new IdleState());
                }
            }
            else
            {
                parent.ChangeState(new NonState());
            }
        }
        public void Exit()
        {
            parent.Agent.ResetPath();
        }
    }

}