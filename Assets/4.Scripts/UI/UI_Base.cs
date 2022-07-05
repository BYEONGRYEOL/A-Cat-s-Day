using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using Isometric.Utility;
using TMPro;

namespace Isometric.UI
{
    public class UI_Base : MonoBehaviour
    {
        // UnityEngine.Object�� ����Ƽ �������� ��� ������Ʈ�� ������Ʈ���� �θ�Ŭ����, �״ϱ� �����̴�.
        // �׸��� �ｼ ���簣�� �� Dictionary�� ��Ƽ� ������ �� �ִ�.
        protected Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
        protected void Bind<T>(Type type) where T : UnityEngine.Object
        {
            string[] names = Enum.GetNames(type);
            UnityEngine.Object[] temp_objects = new UnityEngine.Object[names.Length];
            objects.Add(typeof(T), temp_objects);

            for (int i = 0; i < names.Length; i++)
            {
                if (typeof(T) == typeof(GameObject))
                {
                    temp_objects[i] = Util.FindChild(gameObject, names[i], true);
                }
                else
                {
                    temp_objects[i] = Util.FindChild<T>(gameObject, names[i], true);
                }
                if (temp_objects[i] == null)
                {
                    Debug.Log($"Failed to bind :: {names[i]}");
                }
            }
        }

        public static void BindEvent(GameObject go, Action<PointerEventData> action, Enums.UIEvent type = Enums.UIEvent.Click)
        {
            UI_EventHandler myEvent = Util.GetOrAddComponent<UI_EventHandler>(go);

            switch (type)
            {
                case Enums.UIEvent.Click:
                    myEvent.OnClickHandler -= action;
                    myEvent.OnClickHandler += action;
                    break;
                case Enums.UIEvent.Drag:
                    myEvent.OnDragHandler -= action;
                    myEvent.OnDragHandler += action;
                    break;
            }


        }

        public virtual void Init()
        {

        }
        protected T Get<T>(int index) where T : UnityEngine.Object
        {
            UnityEngine.Object[] temp_objects = null;
            if (objects.TryGetValue(typeof(T), out temp_objects) == false)
            {
                return null;
            }
            return temp_objects[index] as T;
        }
        protected GameObject GetObject(int index) { return Get<GameObject>(index); }
        protected TextMeshProUGUI GetText(int index) { return Get<TextMeshProUGUI>(index); }
        protected Button GetButton(int index) { return Get<Button>(index); }
        protected Image GetImage(int index) { return Get<Image>(index); }

        

    }

}