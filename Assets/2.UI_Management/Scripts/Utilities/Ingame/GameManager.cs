using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Isometric.UI;
using Isometric.Data;

using Isometric.Utility;
namespace Isometric
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        
        /*private static GameManager instance;
        public static GameManager Instance{get{return instance;}}
        private void OnDestroy(){if (instance == this){instance = null;}}
        */
        private GameObject myPlayer;
        

        private string difficultyNow;

        public float inkLeftRatioNow;
        public float inkLeftRatioForStar;
        public float timeLimitForStar;
        public int nowStarCollected;
        public float clearTime;
        public float timeScaleNow;
        public float timeStartBtnPressed;
        public float timeNow;
        private bool stopTimer = false;
        private bool endTimer = false;
        private bool isGameOver;
        
        public bool IsGameOver { get { return isGameOver; } }
        
        protected override void Awake()
        {
            base.Awake();
            
            myPlayer = GameObject.FindGameObjectWithTag("Player");
     
        }

        public void EndLevel()
        {
            
            if (myPlayer != null && Time.timeScale != 0 && !isGameOver)
            {
                //TimeSclae 저장
                timeScaleNow = Time.timeScale;
                Debug.Log("Script :: GameManager // Function :: EndLevel // Parameter :: timeScaleNow is" + timeScaleNow);
                Time.timeScale = 0;
                //남은 잉크 받아오기
                
            }
            
            
            if (!isGameOver)
            {
                nowStarCollected = 0;
                End_Timer();
                isGameOver = true;
                LevelEnded();
                Debug.Log("???????????");
                Time.timeScale = timeScaleNow;
                Debug.Log("Script :: GameManager // Function :: EndLevel // Parameter :: timeScaleNow is" + timeScaleNow);
            

            }
        }
        private void Start()
        {
            
        }
        
        // Update is called once per frame
        private void Update()
        {
            
           
            if (endTimer)
                return;
            Timer();
        }
        
        public void Timer()
        {
            timeNow = Time.time - timeStartBtnPressed;
            if (stopTimer)
            {
                clearTime = timeNow;
                Debug.Log(clearTime);
                endTimer = true;
            }

        }
        public void Start_Timer()
        {
            stopTimer = false;
            timeStartBtnPressed = Time.time;
            Debug.Log("timeStartBtnPressed is" + timeStartBtnPressed);
        }
        private void End_Timer()
        {
            stopTimer = true;
        }
        public void MyPause()
        {
            timeScaleNow = Time.timeScale;
            Time.timeScale = 0;
        }
        public void MyResume()
        {
            Time.timeScale = timeScaleNow;
            Debug.Log("timeScale is" + Time.timeScale);
            UI_Ingame_R.Instance.SceneInitialized();
        }
        private void LevelEnded()
        {
            
            DataManager.Instance.Save();

        }
        
    }
}