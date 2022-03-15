using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Isometric.Utility;

namespace Isometric.Data
{
    public class DataManager : SingletonDontDestroyMonobehavior<DataManager>
    {
        

        public SaveData saveData;
        public JsonSaver jsonSaver;

        public bool TutorialCompleted { get => saveData.tutorialCompleted; set => saveData.tutorialCompleted = value; }
        public bool Mute { get { return saveData.mute; } set { saveData.mute = value; } }
        public float SFXVolume { get { return saveData.sfxVolume; } set { saveData.sfxVolume = value; } }
        public float MusicVolume { get { return saveData.musicVolume; } set { saveData.musicVolume = value; } }
        public string ID { get { return saveData.id; } set { saveData.id = value; } }
        public string Password { get { return saveData.password; } set { saveData.password = value; } }
        public int TotalStars { get { return saveData.totalStars; } set { saveData.totalStars = value; } }
        




        protected override void Awake()
        {
            base.Awake();
           
            saveData = new SaveData();
            jsonSaver = new JsonSaver();
        }

        public void Save()
        {
            Debug.Log("Mute : " + Mute);
            Debug.Log("MusicVolume : " + MusicVolume);
            Debug.Log("SFXVolume : " + SFXVolume);
            Debug.Log("ID : " + ID);
            jsonSaver.Save(saveData);
            
        }

        public void Load()
        {
            Debug.Log("DataManager :: Load");
            jsonSaver.Load(saveData);
        }
    }

}
