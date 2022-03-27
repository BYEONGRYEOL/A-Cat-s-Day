using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class Enemy_HP : Status
    {
        [SerializeField]
        private float hp_max = 100;
        private bool isDamaged = false;

        // Start is called before the first frame update
        void Start()
        {
            base.Initialize(hp_max, hp_max);
        }
        private void Update()
        {
            
        }

        public void HP_Changed(float changeValue)
        {
            MyCurrentValue += changeValue;
        }
      
    }

}