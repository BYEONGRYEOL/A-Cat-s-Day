using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Isometric
{

    public class Enemy_HP_UI : MonoBehaviour
    {
        [SerializeField] private Image hp_bar;
        private float currentFill;
        private float lerpSpeed = 2f;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (currentFill != hp_bar.fillAmount)
            {
                hp_bar.fillAmount = Mathf.Lerp(hp_bar.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }

        public void HP_barSetValue(float currentValue, float maxValue)
        {
            currentFill = currentValue / maxValue;
        }
    }

}