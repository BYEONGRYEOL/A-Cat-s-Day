using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public interface IState
    {
        void Enter(Enemy_1 parent);
        void Update();
        void Exit();

    }

}