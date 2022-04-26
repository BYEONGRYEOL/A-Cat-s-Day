using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometric.Utility;

namespace Isometric.Data
{
    //���� ������ Destroy���������鼭, ��𼭵� ���� �����ؾ��� -> singleton Dontdestroy
    public class DataManager : SingletonDontDestroyMonobehavior<DataManager>
    {
        // ���� ���̺�� �����Ϳ� ���Ͽ� �̸� ����Ǿ��־���ϰ�, �� ������ ���� Ŭ����
        public SaveData saveData;
        // json������� save�ϱ����Ͽ� �ʿ��� json saver
        public JsonSaver jsonSaver;

        // Savedata Ŭ������ �����ϴ� �������� get, set �Ҽ� �ִ� ���������� ����, ���������� savedata�� ���� ���� �������ֱ� ���� �����ϴ� �ڵ� 

        public bool TutorialCompleted { get => saveData.tutorialCompleted; set => saveData.tutorialCompleted = value; }
        public bool Mute { get { return saveData.mute; } set { saveData.mute = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public string ID { get { return saveData.id; } set { saveData.id = value; } }
        public string Password { get { return saveData.password; } set { saveData.password = value; } }
        public int TotalStars { get { return saveData.totalStars; } set { saveData.totalStars = value; } }
        
        //KeyBind ����Ʈ��
        public List<string> KeyBindKeysList { get { return saveData.keyBindKeys; } set { saveData.keyBindKeys = value; } }
        public List<string> ActionBindKeysList { get { return saveData.actionBindKeys; } set { saveData.actionBindKeys = value; } }
        public List<KeyCode> KeyBindValuesList { get { return saveData.keyBindValues; } set { saveData.keyBindValues = value; } }
        public List<KeyCode> ActionBindValuesList { get { return saveData.actionBindValues; } set { saveData.actionBindValues = value; } }


        protected override void Awake()
        {
            //�θ�Ŭ������ �̱��� ����
            base.Awake();
           //savedata, jsonsaver ����
            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            
            Debug.Log("Mute : " + Mute);
            Debug.Log("MusicVolume : " + MusicVolume);
            Debug.Log("SFXVolume : " + SFXVolume);
            Debug.Log("keybinds : " + KeyBindKeysList.Count + " ���� Ű" + KeyBindValuesList.Count + "���� ���");
            //savedata�� jsonsaver�� ������� save
            jsonSaver.Save(saveData);
            
        }

        public void Load()
        {
            Debug.Log("DataManager :: Load");
            //json reader �� ���� ���̺굥���͸� �ε�
            jsonSaver.Load(saveData);   
        }
    }

}
