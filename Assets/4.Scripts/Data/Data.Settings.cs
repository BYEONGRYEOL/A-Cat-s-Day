using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric.Data
{
    [Serializable]
    public class Settings : Data
    {
        //세이브할 모든 데이터들을 선언해놓아야함
        private bool tutorialCompleted;
        private string id;
        private string password;
        private readonly string defaultID = "ID";
        private readonly string defaultPassword = "Password";

        private bool isMute;
        private float sfxVolume;
        private float musicVolume;
        private List<string> keyBindKeys;
        private List<KeyCode> keyBindValues;

        private List<string> actionBindKeys;
        private List<KeyCode> actionBindValues;

        public bool TutorialCompleted { get => tutorialCompleted; set => tutorialCompleted = value; }
        public string Id { get => id; set => id = value; }
        public string Password { get => password; set => password = value; }
        public bool IsMute { get => isMute; set => IsMute = value;  }
        public float SFXVolume { get => sfxVolume; set => sfxVolume = value; }
        public float MusicVolume { get => musicVolume; set => musicVolume = value; }
        public List<string> KeyBindKeys { get => keyBindKeys; set => keyBindKeys = value; }
        public List<KeyCode> KeyBindValues { get => keyBindValues; set => keyBindValues = value; }
        public List<string> ActionBindKeys { get => ActionBindKeys; set => actionBindKeys = value; }
        public List<KeyCode> ActionBindValues { get => actionBindValues; set => actionBindValues = value; }

        


        // 초기화
        public Settings()
        {
            tutorialCompleted = false;
            id = defaultID;
            password = defaultPassword;
            isMute = false;
            sfxVolume = 0f;
            musicVolume = 0f;

            keyBindKeys = new List<string>();
            keyBindValues = new List<KeyCode>();
            actionBindKeys = new List<string>();
            actionBindValues = new List<KeyCode>();
        }
    }

    

}