using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class SceneLoading : SceneBase
    {
        private void Awake()
        {
            Init();
        }
        protected override  void Init()
        {
            base.Init();

            Managers.UI.ShowSceneUI<UI_Loading>();
            Debug.Log("�����̰Եι�?");
        }

        public override void Clear()
        {
            
        }
    }

}