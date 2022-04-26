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
            base.Init(hp_max, hp_max);
        }
       

        public void HP_Changed(float changeValue)
        {
            MyCurrentValue += changeValue;
            UI_Ingame_R.Instance.HP_barSetValue(MyCurrentValue, MyMaxValue);
        }
        
        
    }

}