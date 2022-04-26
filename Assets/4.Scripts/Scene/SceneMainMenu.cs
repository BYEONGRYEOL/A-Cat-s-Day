using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class SceneMainMenu : SceneBase
    {
        public override void Clear()
        {
            
        }

        protected override void Init()
        {
            base.Init();

            Managers.UI.ShowSceneUI<UI_MainMenu>();
        }

    }

}