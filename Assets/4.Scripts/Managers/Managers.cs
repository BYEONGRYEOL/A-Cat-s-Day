using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
namespace Isometric
{
    public class Managers : MonoBehaviour
    {
        static Managers instance;
        static Managers Instance { get { Init(); return instance; } }

        KeyBindManager _keyBind = new KeyBindManager();
        InputManager _input = new InputManager();
        ResourceManager _resource = new ResourceManager();
        UIManager _ui = new UIManager();
        MySceneManager _scene = new MySceneManager();

        public static KeyBindManager KeyBind { get { return Instance._keyBind; } }
        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static MySceneManager Scene { get { return Instance._scene; } }

        void Awake()
        {
            Init();
            KeyBind.Init();
        }

        void Update()
        {
            _input.OnUpdate();
        }

        static void Init()
        {
            // ���� Manager ���� ������Ʈ�� ���ٸ�
            if(instance == null)
            {
                GameObject go = GameObject.Find("@Managers");
                if(go == null)
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                instance = go.GetComponent<Managers>();
            }
        }
        public static void Clear()
        {
            Input.Clear();
        }

       
    } 
}