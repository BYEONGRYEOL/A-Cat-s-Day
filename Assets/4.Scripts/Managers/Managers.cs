using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.UI;
using Isometric.Data;

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
        DataManager _data = new DataManager();
        PoolManager _pool = new PoolManager();
        TimeManager _time = new TimeManager();
        GameManager _game = new GameManager();
        InventoryManager _inven = new InventoryManager();

        public static KeyBindManager KeyBind { get { return Instance._keyBind; } }
        public static InputManager Input { get { return Instance._input; } }
        public static ResourceManager Resource { get { return Instance._resource; } }
        public static UIManager UI { get { return Instance._ui; } }
        public static MySceneManager Scene { get { return Instance._scene; } }
        public static DataManager Data { get { return Instance._data; } }
        public static PoolManager Pool { get { return Instance._pool; } }
        public static TimeManager Time { get { return Instance._time; } }
        public static GameManager Game { get { return Instance._game; } }
        public static InventoryManager Inven { get { return Instance._inven; } }
        void Awake()
        {
            Init();
            
        }

        void Update()
        {
            _input.OnUpdate();
            _time.OnUpdate();
            _game.OnUpdate();
        }

        static void Init()
        {
            // 씬에 Manager 게임 오브젝트가 없다면
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

                instance._pool.Init();
                
                instance._data.Init();
                
                instance._keyBind.Init();

                instance._inven.Init();
                
            }
        }
        public static void Clear()
        {
            Input.Clear();
            Pool.Clear();
        }

        

    } 
}