using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class FightState : IState
    {
        private Enemy parent;
        public void Enter(Enemy parent)
        {
            Debug.Log("Enter FightState");

            this.parent = parent;
        }

        // Update is called once per frame
        public void Update()
        {
            if(parent.Target != null)
            {
                if (!parent.IsAttacking && parent.DetermineState() == 2)
                {
                    parent.FightAction();
                }

                if (parent.DetermineState() == 1 && !parent.IsAttacking)
                {
                    parent.ChangeState(new ChaseState());
                }
            }
            else
            {
                parent.ChangeState(new IdleState());
            }
        }
        public void Exit()
        {

        }
    }

}