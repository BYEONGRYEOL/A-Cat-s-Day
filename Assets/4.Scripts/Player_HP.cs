using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{

    public class Player_HP : Status
    {
        [SerializeField]
        private float hp_max = 100;
        
        // Start is called before the first frame update
        void Start()
        {
            base.Initialize(hp_max, hp_max);
        }
        private void Update()
        {
            Debuging();
        }

        public void HP_Changed(float changeValue)
        {
            MyCurrentValue += changeValue;
            UI_Ingame_R.Instance.HP_bar(MyCurrentValue, MyMaxValue);
        }
        
        void Debuging()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                HP_Changed(-1);
            }
        }
    }

}