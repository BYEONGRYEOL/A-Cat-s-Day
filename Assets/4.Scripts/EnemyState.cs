using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class EnemyState : IState
    {
        private EnemyAI parent;
        public void Enter(EnemyAI parent)
        {
            this.parent = parent;
        }
        public void Exit()
        {

        }
        public void Update()
        {

        }
    }

}