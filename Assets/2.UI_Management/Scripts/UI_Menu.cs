using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
namespace Isometric.UI
{
    public abstract class UI_Menu<T> : UI_Menu where T: UI_Menu<T>
    {
        private static T instance;
        public static T Instance { get { return instance; } }
        protected virtual void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = (T)this;
            }
        }

        protected virtual void Ondestroy()
        {
            Debug.Log("Script :: UI_Menu // Function :: Ondestroy");
            instance = null;
        }

        public static void Open()
        {
            SFX.Instance.PlaySFX("BtnClick");

            Debug.Log("Script :: UI_Menu // Function :: Open");
            if(UI_Manager.Instance != null && Instance != null)
            {
                UI_Manager.Instance.OpenMenu(Instance);

            }
        }
    }
    [RequireComponent(typeof(Canvas))]
    public abstract class UI_Menu : MonoBehaviour
    {
        public virtual void OnBtnBackPressed()
        {
            SFX.Instance.PlaySFX("BtnBackClick");
            Debug.Log("Script :: UI_Menu // Function :: OnBackPressed");
            if (UI_Manager.Instance != null)
            {
                UI_Manager.Instance.CloseMenu();
            }
        }
        
        // Start is called before the first frame update

    }

}
