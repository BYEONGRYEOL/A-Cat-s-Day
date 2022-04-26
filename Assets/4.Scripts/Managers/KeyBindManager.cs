using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using Isometric.Data;
using System;
using System.Linq;
namespace Isometric
{

    public class KeyBindManager
    {
        private string bindName;

        public List<string> keyBindKeys;
        public List<KeyCode> keyBindValues;
        public List<string> actionBindKeys;
        public List<KeyCode> actionBindValues;

        public Dictionary<string, KeyCode> KeyBinds { get; set; }
        public Dictionary<string, KeyCode> ActionBinds { get; set; }
        
        string GetKeyName(Enums.Key type)
        {
            string name = System.Enum.GetName(typeof(Enums.Key), type);
            return name;
        }
        public void BindKey(string key, KeyCode keyBind)
        {
            Debug.Log("BindKey 실행");
            // Keybinds 와 ActionBinds에 bind 하는걸 하나의 함수에서 할 수 있게 현재 딕셔너리 선언
            Dictionary<string, KeyCode> currentDictionary = KeyBinds;
            Debug.Log("현재 딕셔너리 선언");
            
            // Action이 포함된 경우 ActionBinds 
            if (key.Contains("ACTION_"))
            {
                Debug.Log("액션을 포함한다면");
                currentDictionary = ActionBinds;
            }
            Debug.Log("첫 if 문 통과");
            // 입력받은 key 에 해당하는 value가 없을 경우 그냥 추가만
            if (!currentDictionary.ContainsKey(key))
            {
                Debug.Log("아직 키가 없다면");
                currentDictionary.Add(key, keyBind);
            }
            
            // 입력받은 key 에 해당하느 ㄴvalue가 있을경우 하나의 버튼으로 두 개의 조작키가 눌리면 안되니까 바꿔줘용
            else if (currentDictionary.ContainsValue(keyBind))
            {
                Debug.Log("키에 해당하는 밸류가 있다면");
                string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
                Debug.Log("찾아서");
                currentDictionary[myKey] = KeyCode.None;

            }
            Debug.Log("키에 맞는 밸류 넣기");
            currentDictionary[key] = keyBind;
            bindName = string.Empty;

            //save

        }
        
        public void Init()
        {
            BindKey("UP", KeyCode.W);
            BindKey("DOWN", KeyCode.S);
            BindKey("RIGHT", KeyCode.D);
            BindKey("LEFT", KeyCode.A);

            BindKey("RUN", KeyCode.LeftShift);
            BindKey("CROUCH", KeyCode.LeftControl);
            BindKey("INTERACTION", KeyCode.G);
            BindKey("ACTION_1", KeyCode.Alpha1);
            BindKey("ACTION_2", KeyCode.Alpha2);
            BindKey("ACTION_3", KeyCode.Alpha3);
            BindKey("ACTION_4", KeyCode.Alpha4);


            if (DataManager.Instance != null)
            {
                DataManager.Instance.KeyBindKeysList = KeyBinds.Keys.ToList();
                DataManager.Instance.ActionBindKeysList = ActionBinds.Keys.ToList();
                DataManager.Instance.KeyBindValuesList = KeyBinds.Values.ToList();
                DataManager.Instance.ActionBindValuesList = ActionBinds.Values.ToList();
            }
        }
        public void SaveData()
        {
            if (DataManager.Instance != null)
            {
                DataManager.Instance.KeyBindKeysList = KeyBinds.Keys.ToList();
                DataManager.Instance.ActionBindKeysList = ActionBinds.Keys.ToList();
                DataManager.Instance.KeyBindValuesList = KeyBinds.Values.ToList();
                DataManager.Instance.ActionBindValuesList = ActionBinds.Values.ToList();
            }
            //Debug.Log("DataManager.Instance.MusicVolume is" + DataManager.Instance.MusicVolume);
            if (DataManager.Instance != null)
            {
                DataManager.Instance.Save();
            }
        }
        public void LoadData()
        {
            KeyBinds = new Dictionary<string, KeyCode>();
            ActionBinds = new Dictionary<string, KeyCode>();
            if (DataManager.Instance == null)
            {
                return;
            }
            DataManager.Instance.Load();

            if (DataManager.Instance.KeyBindKeysList.Count == 0)
            {
                Debug.Log("키 설정 초기화 실행");
                Init();
            }
            else
            {
                Debug.Log("데이터 로드");

                keyBindKeys = DataManager.Instance.KeyBindKeysList;
                keyBindValues = DataManager.Instance.KeyBindValuesList;
                actionBindKeys = DataManager.Instance.ActionBindKeysList;
                actionBindValues = DataManager.Instance.ActionBindValuesList;


                for (int i = 0; i < keyBindKeys.Count; i++)
                {
                    Debug.Log(keyBindKeys + "keybindkeys");
                    Debug.Log(keyBindKeys.Count);
                    Debug.Log(keyBindKeys[i]);
                    Debug.Log(keyBindValues[i]);
                    KeyBinds.Add(keyBindKeys[i], keyBindValues[i]);

                    BindKey(KeyBinds.Keys.ToList()[i], KeyBinds[KeyBinds.Keys.ToList()[i]]);
                }
                for (int i = 0; i < actionBindKeys.Count; i++)
                {
                    ActionBinds.Add(actionBindKeys[i], actionBindValues[i]);

                    BindKey(ActionBinds.Keys.ToList()[i], ActionBinds[ActionBinds.Keys.ToList()[i]]);
                }


            }
        }
    }

}