using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Isometric.Utility;

namespace Isometric.UI
{
    
    public class UI_Manager : SingletonDontDestroyMonobehavior<UI_Manager>
    {
        
        [SerializeField] private UI_MainMenu mainMenuPrefab;
        [SerializeField] private UI_Option optionPrefab;
        
        [SerializeField] private UI_Ingame_R ingamePrefab_R;
        [SerializeField] private UI_About aboutPrefab;
        [SerializeField] private UI_Login loginPrefab;


        [SerializeField] private Transform myMenuParent;

        #region ΩÃ±€≈Ê 
        
        #endregion ΩÃ±€≈Ê
        private Stack<UI_Menu> myMenuStack = new Stack<UI_Menu>();
        
        protected override void Awake()
        {

            base.Awake();
            InitializeMenus();
        }

        private void InitializeMenus()
        {
            if(myMenuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                myMenuParent = menuParentObject.transform;
            }
            DontDestroyOnLoad(myMenuParent.gameObject);
            
            System.Type myType = this.GetType();

            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = myType.GetFields(myFlags);


            foreach(FieldInfo field in fields)
            {
                UI_Menu prefab = field.GetValue(this) as UI_Menu;

                if(prefab != null)
                {
                    UI_Menu menuInstance = Instantiate(prefab, myMenuParent);
                    
                    if(prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(menuInstance);
                    }
                }
            }
        }
       
        public void OpenMenu(UI_Menu menuInstance)
        {

            if (menuInstance == null)
            {
                Debug.LogWarning("Script MenuManager Function OpenMenu Error : invalid menu");
                return;
            }
            if(myMenuStack.Count > 0)
            {
                foreach(UI_Menu menu in myMenuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }
            menuInstance.gameObject.SetActive(true);
            myMenuStack.Push(menuInstance);


            string a = menuInstance.name;
            Run_OpenMenu(a);
            
            /*a = "";
            menuName = "";*/
        }
        public void Run_OpenMenu(string a)
        {
            string menuName = a.Remove(a.IndexOf("("));
            Debug.Log(menuName);
            if (menuName == "UI_Login")
            {
                UI_Login.Instance.OpenMenu();
            }
            else if (menuName == "UI_Menu")
            {
                UI_MainMenu.Instance.OpenMenu();
            }
            else if (menuName == "UI_Option")
            {
                UI_Option.Instance.OpenMenu();
            }
            
            else if (menuName == "UI_Score")
            {
                UI_Score.Instance.OpenMenu();
            }
           
            else if (menuName == "UI_Ingame_R")
            {
                UI_Ingame_R.Instance.OpenMenu();
            }
      
            else if (menuName == "UI_About")
            {
                UI_About.Instance.OpenMenu();
            }
        }
        public void CloseMenu()
        {
            if(myMenuStack.Count == 0)
            {
                Debug.LogWarning("Scirpt MenuManager Function CloseMenu ERROR : there's no menu to close in stack");
                return;
            }

            UI_Menu topMenu = myMenuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if(myMenuStack.Count > 0)
            {
                UI_Menu nextMenu = myMenuStack.Peek();
                nextMenu.gameObject.SetActive(true);
                Run_OpenMenu(nextMenu.name);
            }
        }
    }

}
