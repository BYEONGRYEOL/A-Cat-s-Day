using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class SceneGame : SceneBase
    {
        UI_Inventory inventory;
        enum PopUpKeys
        {
            
        }
        protected override void Init()
        {
            base.Init();
            

            
            inventory = GetComponent<UI_Inventory>();

            SceneType = Enums.Scene.SceneGame;
        }
        public override void Clear()
        {
            throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }

}