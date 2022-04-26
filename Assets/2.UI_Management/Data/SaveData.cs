using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Isometric.Data
{
    
    public class SaveData
    {
        //세이브할 모든 데이터들을 선언해놓아야함
        public bool tutorialCompleted;
        public string id;
        public string password;
        private readonly string defaultID = "ID";
        private readonly string defaultPassword = "Password";
       
        public int totalStars;
        public bool mute;
        public float sfxVolume;
        public float musicVolume;

        public List<string> keyBindKeys;
        public List<KeyCode> keyBindValues;

        public List<string> actionBindKeys;
        public List<KeyCode> actionBindValues;
        
        
        // 초기화
        public SaveData()
        {
            tutorialCompleted = false;
            id = defaultID;
            password = defaultPassword;
            mute = false;
            sfxVolume = 0f;
            musicVolume = 0f;
            keyBindKeys = new List<string>();
            keyBindValues = new List<KeyCode>();
            actionBindKeys = new List<string>();
            actionBindValues = new List<KeyCode>();
            
            totalStars = default;
            
        }
    }
}