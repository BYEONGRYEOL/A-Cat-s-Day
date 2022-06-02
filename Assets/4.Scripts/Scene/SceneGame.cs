using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class SceneGame : SceneBase
    {
        enum PopUpKeys
        {
            
        }
        protected override void Init()
        {
            base.Init();
            Debug.Log("GameScene Load!");
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