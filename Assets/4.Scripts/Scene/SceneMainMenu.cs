using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class SceneMainMenu : SceneBase
    {
        IEnumerator LoadJson()
        {
            Debug.Log("Main Menu :: LoadJson run");
            yield return new WaitUntil(() => Managers.Data.IsJsonLoaded());
            Debug.Log("Main Menu :: MakeJsontoDict run");
            Managers.Data.MakeJsontoDict();
            Managers.Data.MakeJsontoList();
        }
        public override void Clear()
        {
            
        }

        protected override void Init()
        {
            base.Init();
            StartCoroutine(LoadJson());
            Managers.UI.ShowSceneUI<UI_MainMenu>();
        }

    }

}