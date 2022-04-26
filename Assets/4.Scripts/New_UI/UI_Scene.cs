using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.UI
{
    public class UI_Scene : UI_Base
    {
        protected virtual void Init()
        {
            Managers.UI.SetCanvas(gameObject, false);
        }
    }

}