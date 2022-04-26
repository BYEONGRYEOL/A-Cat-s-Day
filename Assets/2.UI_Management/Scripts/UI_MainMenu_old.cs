using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using Isometric.Utility;
using Isometric.Data;
namespace Isometric.UI
{
    public class UI_MainMenu_old : UI_Menu<UI_MainMenu_old>
    {
        [SerializeField] TransitionFader transitionPrefab;
        
        private float musicVolume;

        public void OpenMenu()
        {
            Debug.Log("WOW");
            DataManager.Instance.Load();
            Managers.KeyBind.LoadData();
            musicVolume = DataManager.Instance.MusicVolume;
            BGM.Instance.Volume(musicVolume);
            SFX.Instance.Volume(DataManager.Instance.SFXVolume);
        }
        
        public void OnBtnPlayPressed()
        {
            Debug.Log("Script :: UI_MainMenu / Function :: OnPlayPressed");
            UI_Ingame_R.Open();
            //LevelLoader.LoadLevel("Ingame");
        }
       


        //Quit.
        public override void OnBtnBackPressed()
        {
            Debug.Log("Script :: UI_MainMenu / Function :: OnBackPressed");
            Application.Quit();
        }

        
    }
}