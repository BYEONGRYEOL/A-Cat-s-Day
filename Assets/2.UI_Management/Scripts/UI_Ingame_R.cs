using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using Isometric.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;


namespace Isometric.UI
{
    public class UI_Ingame_R : UI_Menu<UI_Ingame_R>
    {
        Player_HP hp;
        #region SerializeField
        [SerializeField] private Image hp_bar;
        [SerializeField] private TextMeshProUGUI hp_Text;
        #endregion

        private float currentFill;
        private float lerpSpeed = 3f;

        private bool isStarted = false;

        private bool seeTutorial = true;
        private int tutorialStep = 0;
        private bool isBtnTutorialActivate = true;

        public bool IsStarted => isStarted;
        public float delay = 0.4f;
        public float myTimeScale = 1f;
        protected override void Awake()
        {
            base.Awake();
            
        }
        private void Update()
        {
            if(currentFill != hp_bar.fillAmount)
            {
                hp_bar.fillAmount = Mathf.Lerp(hp_bar.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
        }
        public void OpenMenu()
        {
            Debug.Log("Script :: UI_Ingame_R / Function :: OnEnable");
            hp = GetComponent<Player_HP>();
        }   
        public void SceneInitialized()
        {
            Debug.Log("Script :: UI_Ingame_R / Function :: SceneInitialized");
            
        }
        public void HP_bar(float currentValue, float maxValue)
        {
            currentFill = currentValue / maxValue;
            hp_Text.text = currentValue + "/" + maxValue;
        }
        
    }

}
