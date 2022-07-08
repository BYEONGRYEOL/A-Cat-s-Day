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

            //매니저를 처음 불러와 실행함으로써 초기화
            Managers.UI.ShowSceneUI<UI_Loading>();
            Debug.Log("설마이게두번?");
        }

        public override void Clear()
        {
            //DataManager는 로딩 씬이 넘어가면 Dict를 갖고있음
            
        }
    }

}