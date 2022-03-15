using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric
{
    public class Status : MonoBehaviour
    {
        
        [SerializeField]
        
        private float currentFill;
        public float MyMaxValue { get; set; }

        public float MyCurrentValue
        {
            get
            {
                return currentValue;
            }

            set
            {
                if (value > MyMaxValue) currentValue = MyMaxValue;
                else if (value < 0) currentValue = 0;
                else currentValue = value;
            }
        }

        private float currentValue;

        void Start()
        {

        }
        void Update()
        {
            
        }

        public void Initialize(float currentValue, float maxValue)
        {
            MyMaxValue = maxValue;
            MyCurrentValue = currentValue;
        }
    }

}