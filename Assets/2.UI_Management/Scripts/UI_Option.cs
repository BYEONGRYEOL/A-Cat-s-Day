using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using UnityEngine.UI;
using Isometric.Data;

namespace Isometric.UI 
{

    public class UI_Option : UI_Menu<UI_Option>
    {
        private bool isMute;
        [SerializeField] private Button btn_Mute;
        [SerializeField] private Slider slider_SFXVolume;
        [SerializeField] private Slider slider_MusicVolume;
        
        
        protected override void Awake()
        {
            base.Awake();
            

        }

        public void OpenMenu()
        {
            LoadData();
            if (!isMute)
            {
                btn_Mute.gameObject.SetActive(false);
            }
            else
            {
                btn_Mute.gameObject.SetActive(true);
            }
        }
        

        public void OnSFXVolumeChanged(float volume)
        {
            /*PlayerPrefs.SetFloat("SFXVolume", volume);*/
            if (DataManager.Instance != null)
            {
                DataManager.Instance.SFXVolume = volume;
                SFX.Instance.Volume(volume);

            }
        }

        public void OnMusicVolumeChanged(float volume)
        {
            /*PlayerPrefs.SetFloat("MusicVolume", volume);*/
            if (DataManager.Instance != null)
            {
                Debug.Log("DataManager.Instance is not null");
                DataManager.Instance.MusicVolume = volume;
                Debug.Log(DataManager.Instance.MusicVolume);
            }
            BGM.Instance.Volume(volume);
        }
        public override void OnBtnBackPressed()
        {
            
            //Debug.Log("DataManager.Instance.MusicVolume is" + DataManager.Instance.MusicVolume);
            if (DataManager.Instance != null)
            {
                DataManager.Instance.Save();
            }
            base.OnBtnBackPressed();
            

            /*PlayerPrefs.Save();*/
            
        }

        public void OnBtnUnMutePressed()
        {
            if (isMute)
            {
                if (DataManager.Instance != null)
                {
                    isMute = false;
                }
                
                btn_Mute.gameObject.SetActive(false);
                BGM.Instance.UnMute();
            }
            else
            {
                if (DataManager.Instance != null)
                {
                    isMute = true;
                }
                btn_Mute.gameObject.SetActive(true);
                BGM.Instance.Mute();

            }
        }
        

        
        public void LoadData()
        {
            if(DataManager.Instance == null ||slider_MusicVolume == null|| slider_SFXVolume == null)
            {
                return;
            }
            DataManager.Instance.Load();
            isMute = DataManager.Instance.Mute;
            slider_SFXVolume.value = DataManager.Instance.SFXVolume;
            slider_MusicVolume.value = DataManager.Instance.MusicVolume;
            
        }
    }

}

