using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Isometric.Data;
namespace Isometric.UI
{
    public class UI_Login_old : UI_Menu<UI_Login_old>
    {
        private Button button_login;
        [SerializeField] private InputField inputField_ID;
        
        
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {

        }
        public void OpenMenu()
        {
            Debug.Log("WOW");
            LoadData();

        }

        private void LoadData()
        {
            Debug.Log("DataManager.Instance is null? " + DataManager.Instance == null);
            
            Debug.Log("Script :: UI_Login / Function :: LoadData");
            DataManager.Instance.Load();
                
            inputField_ID.text = DataManager.Instance.ID;
            Debug.Log("inputField_ID.text is " + inputField_ID.text);    
            
        }
        public void OnIDValueChanged(string id)
        {
            if(DataManager.Instance != null)
            {

                DataManager.Instance.ID = id;
                
            }
        }

        public void OnEndEdit()
        {
            if(DataManager.Instance != null)
            {
                DataManager.Instance.Save();
            }
        }
        public void OnPasswordValueChanged(string password)
        {
            if (DataManager.Instance != null)
            {
                Debug.Log("OnPasswrodValueChanged");
                DataManager.Instance.Password = password;
            }
        }

        public void OnBtnLoginClick()
        {
            
            DataManager.Instance.Save();
           
            UI_MainMenu_old.Open();
        }

        
    }
}