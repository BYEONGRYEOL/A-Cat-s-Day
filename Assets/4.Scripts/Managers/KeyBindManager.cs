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
            Debug.Log("BindKey ����");
            // Keybinds �� ActionBinds�� bind �ϴ°� �ϳ��� �Լ����� �� �� �ְ� ���� ��ųʸ� ����
            Dictionary<string, KeyCode> currentDictionary = KeyBinds;
            Debug.Log("���� ��ųʸ� ����");
            
            // Action�� ���Ե� ��� ActionBinds 
            if (key.Contains("ACTION_"))
            {
                Debug.Log("�׼��� �����Ѵٸ�");
                currentDictionary = ActionBinds;
            }
            Debug.Log("ù if �� ���");
            // �Է¹��� key �� �ش��ϴ� value�� ���� ��� �׳� �߰���
            if (!currentDictionary.ContainsKey(key))
            {
                Debug.Log("���� Ű�� ���ٸ�");
                currentDictionary.Add(key, keyBind);
            }
            
            // �Է¹��� key �� �ش��ϴ� ��value�� ������� �ϳ��� ��ư���� �� ���� ����Ű�� ������ �ȵǴϱ� �ٲ����
            else if (currentDictionary.ContainsValue(keyBind))
            {
                Debug.Log("Ű�� �ش��ϴ� ����� �ִٸ�");
                string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;
                Debug.Log("ã�Ƽ�");
                currentDictionary[myKey] = KeyCode.None;

            }
            Debug.Log("Ű�� �´� ��� �ֱ�");
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
                Debug.Log("Ű ���� �ʱ�ȭ ����");
                Init();
            }
            else
            {
                Debug.Log("������ �ε�");

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