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

        // 옵션 창이 열리면, 일시정지
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

        public void OnSavePressed()
        {
            DataManager.Instance.Save();
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
        // 옵션창에서 뒤로가기 버튼을 누른다는 건 게임 재개를 뜻함 
        public override void OnBtnBackPressed()
        {
            GameManager.Instance.MyResume();
            //Debug.Log("DataManager.Instance.MusicVolume is" + DataManager.Instance.MusicVolume);
            if (DataManager.Instance != null)
            {
                DataManager.Instance.Save();
            }
            // 뒤로가기 이전에 구현해놔야 실행되어용
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
        
        public void OpenKeyBinds()
        {
            UI_KeyBinds.Open();
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

