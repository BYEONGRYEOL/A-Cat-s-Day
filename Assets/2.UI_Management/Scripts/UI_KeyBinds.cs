using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using UnityEngine.UI;
using Isometric.Data;
using TMPro;
using System;
using System.Linq;

namespace Isometric.UI
{


    public class UI_KeyBinds : UI_Menu<UI_KeyBinds>
    {
        private readonly Dictionary<char, KeyCode> _keycodeCache = new Dictionary<char, KeyCode>();
        private KeyCode GetKeyCode(char character)
        {
            // Get from cache if it was taken before to prevent unnecessary enum parse
            KeyCode code;
            if (_keycodeCache.TryGetValue(character, out code)) return code;
            // Cast to it's integer value
            int alphaValue = character;
            code = (KeyCode)Enum.Parse(typeof(KeyCode), alphaValue.ToString());
            _keycodeCache.Add(character, code);
            return code;
        }
        // keybind 버튼들을 게임오브젝트 배열로 받기위해 선언
        private GameObject[] keybindButtons;

        // 버튼의 씬뷰 이름을 key값 으로, 실제 부여할 KeyCode를 value 값으로 받기위해 셋팅
        // Action 들은 실제 UI에서 표시해야할 수 있는 점에서 KeyBinds와 달라 두 개의 다른 딕셔너리 선언

        private List<string> keyBindKeys;
        private List<KeyCode> keyBindValues;
        private List<string> actionBindKeys;
        private List<KeyCode> actionBindValues;

        public Dictionary<string, KeyCode> KeyBinds { get; set; }
        public Dictionary<string, KeyCode> ActionBinds { get; set; }

        private string bindName;

        protected override void Awake()
        {
            base.Awake();
        }

        //조작Key를 설정하는 함수
        public void BindKey(string key, KeyCode keyBind)
        {
            Debug.Log("BindKey 실행");
            // Keybinds 와 ActionBinds에 bind 하는걸 하나의 함수에서 할 수 있게 현재 딕셔너리 선언
            Dictionary<string, KeyCode> currentDictionary = KeyBinds;
            // Action이 포함된 경우 ActionBinds 
            if (key.Contains("ACTION_"))
            {
                currentDictionary = ActionBinds;
            }
            // 입력받은 key 에 해당하는 value가 없을 경우 그냥 추가만
            if(!currentDictionary.ContainsKey(key))
            {
                currentDictionary.Add(key, keyBind);
                UpdateKeyText(key, keyBind);
            }
            // 입력받은 key 에 해당하느 ㄴvalue가 있을경우 하나의 버튼으로 두 개의 조작키가 눌리면 안되니까 바꿔줘용
            else if (currentDictionary.ContainsValue(keyBind))
            {
                string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
            
                currentDictionary[myKey] = KeyCode.None;
                UpdateKeyText(key, KeyCode.None);

            }
            currentDictionary[key] = keyBind;
            UpdateKeyText(key, keyBind);
            bindName = string.Empty;

            //save
            
        }

        public void OnKeyBindBtnClick(string bindName)
        {
            
            this.bindName = bindName;
        }


        
        // 매 프레임 마다 호출됨
        private void OnGUI()
        {
            if(bindName != string.Empty)
            {
                Event e = Event.current;

                if (e.isKey)
                {
                    BindKey(bindName, e.keyCode);
                }
            }
        }

        public void OpenMenu()
        {
            KeyBinds = new Dictionary<string, KeyCode>();
            ActionBinds = new Dictionary<string, KeyCode>();
            keybindButtons = GameObject.FindGameObjectsWithTag("KeyBind");
            
            LoadData();
        }
        private void Initialize()
        {
            Managers.KeyBind.Init();
        }
        // 게임오브젝트 배열로 받아온 버튼들의 이름을 참조해, 해당하는 버튼의 자식 오브젝트인 TMP에 표시
        public void UpdateKeyText(string key, KeyCode code)
        {
            Debug.Log("UpdateKeyText 실행");
            TextMeshProUGUI tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<TextMeshProUGUI>();
            tmp.text = code.ToString();
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
            Managers.KeyBind.SaveData();
            base.OnBtnBackPressed();


            /*PlayerPrefs.Save();*/

        }

        public void OnBtnUnMutePressed()
        {
            
        }

        public void LoadData()
        {
            Managers.KeyBind.LoadData();
        }
    }

}

