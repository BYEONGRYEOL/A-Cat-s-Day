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
        // keybind ��ư���� ���ӿ�����Ʈ �迭�� �ޱ����� ����
        private GameObject[] keybindButtons;

        // ��ư�� ���� �̸��� key�� ����, ���� �ο��� KeyCode�� value ������ �ޱ����� ����
        // Action ���� ���� UI���� ǥ���ؾ��� �� �ִ� ������ KeyBinds�� �޶� �� ���� �ٸ� ��ųʸ� ����

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

        //����Key�� �����ϴ� �Լ�
        public void BindKey(string key, KeyCode keyBind)
        {
            Debug.Log("BindKey ����");
            // Keybinds �� ActionBinds�� bind �ϴ°� �ϳ��� �Լ����� �� �� �ְ� ���� ��ųʸ� ����
            Dictionary<string, KeyCode> currentDictionary = KeyBinds;
            // Action�� ���Ե� ��� ActionBinds 
            if (key.Contains("ACTION_"))
            {
                currentDictionary = ActionBinds;
            }
            // �Է¹��� key �� �ش��ϴ� value�� ���� ��� �׳� �߰���
            if(!currentDictionary.ContainsKey(key))
            {
                currentDictionary.Add(key, keyBind);
                UpdateKeyText(key, keyBind);
            }
            // �Է¹��� key �� �ش��ϴ� ��value�� ������� �ϳ��� ��ư���� �� ���� ����Ű�� ������ �ȵǴϱ� �ٲ����
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


        
        // �� ������ ���� ȣ���
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
        // ���ӿ�����Ʈ �迭�� �޾ƿ� ��ư���� �̸��� ������, �ش��ϴ� ��ư�� �ڽ� ������Ʈ�� TMP�� ǥ��
        public void UpdateKeyText(string key, KeyCode code)
        {
            Debug.Log("UpdateKeyText ����");
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

