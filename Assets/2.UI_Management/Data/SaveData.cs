using System.Collections;
using System.Collections.Generic;
using System;

namespace Isometric.Data
{
    
    public class SaveData
    {
        public bool tutorialCompleted;
        public string id;
        public string password;
        private readonly string defaultID = "ID";
        private readonly string defaultPassword = "Password";
       
        public int totalStars;
        public bool mute;
        public float sfxVolume;
        public float musicVolume;

        

        public SaveData()
        {
            tutorialCompleted = false;
            id = defaultID;
            password = defaultPassword;
            mute = false;
            sfxVolume = 0f;
            musicVolume = 0f;
            
            totalStars = default;
            
        }
    }
}