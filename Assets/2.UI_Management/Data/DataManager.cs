using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometric.Utility;

namespace Isometric.Data
{
    //게임 진행중 Destroy되지않으면서, 어디서든 쉽게 참조해야함 -> singleton Dontdestroy
    public class DataManager : SingletonDontDestroyMonobehavior<DataManager>
    {
        // 실제 세이브될 데이터에 대하여 미리 선언되어있어야하고, 그 내용을 담은 클래스
        public SaveData saveData;
        // json방식으로 save하기위하여 필요한 json saver
        public JsonSaver jsonSaver;

        // Savedata 클래스에 존재하는 변수들을 get, set 할수 있는 전역변수로 선언, 실질적으로 savedata의 변수 값을 설정해주기 위해 존재하는 코드 

        public bool TutorialCompleted { get => saveData.tutorialCompleted; set => saveData.tutorialCompleted = value; }
        public bool Mute { get { return saveData.mute; } set { saveData.mute = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public string ID { get { return saveData.id; } set { saveData.id = value; } }
        public string Password { get { return saveData.password; } set { saveData.password = value; } }
        public int TotalStars { get { return saveData.totalStars; } set { saveData.totalStars = value; } }
        
        //KeyBind 리스트들
        public List<string> KeyBindKeysList { get { return saveData.keyBindKeys; } set { saveData.keyBindKeys = value; } }
        public List<string> ActionBindKeysList { get { return saveData.actionBindKeys; } set { saveData.actionBindKeys = value; } }
        public List<KeyCode> KeyBindValuesList { get { return saveData.keyBindValues; } set { saveData.keyBindValues = value; } }
        public List<KeyCode> ActionBindValuesList { get { return saveData.actionBindValues; } set { saveData.actionBindValues = value; } }


        protected override void Awake()
        {
            //부모클래스의 싱글톤 구현
            base.Awake();
           //savedata, jsonsaver 선언
            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            
            Debug.Log("Mute : " + Mute);
            Debug.Log("MusicVolume : " + MusicVolume);
            Debug.Log("SFXVolume : " + SFXVolume);
            Debug.Log("keybinds : " + KeyBindKeysList.Count + " 개의 키" + KeyBindValuesList.Count + "개의 밸류");
            //savedata를 jsonsaver의 방식으로 save
            jsonSaver.Save(saveData);
            
        }

        public void Load()
        {
            Debug.Log("DataManager :: Load");
            //json reader 를 거쳐 세이브데이터를 로드
            jsonSaver.Load(saveData);   
        }
    }

}
