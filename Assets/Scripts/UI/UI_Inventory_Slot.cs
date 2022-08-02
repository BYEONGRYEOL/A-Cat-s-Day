using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Isometric.UI
{

    public class UI_Inventory_Slot : UI_Base
    { 
        public Image icon;
        private bool hasItem = false;
        public bool HasItem { get { return hasItem; } set { hasItem = value; } }
        private void Start()
        {
            Init();
        }
        public override void Init()
        {
            //BindEvent
            BindEvent(this.gameObject, PointerEventData => IconDragging(), type:Enums.UIEvent.Drag);
            
        }

        public void IconDragging()
        {
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
            CursorController.Instance.OnGrabbed(icon);
        }
        public void SetItem(int templateid, int count)
        {
            Data.ItemInfo itemInfo = null;
            Debug.Log("try to getvalue :: item templateid is ::" + templateid);
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);
            Debug.Log("itemInfo is ::::" + itemInfo.name );

            Sprite[] itemicons = Resources.LoadAll<Sprite>("New/Test/Fantastic UI Starter Pack");
            Debug.Log(itemicons +"itemicon multiple sprite 정상 로드" +itemicons.Length);
            Debug.Log(itemInfo.resourcePath);
            string resourcePath = itemInfo.resourcePath;
            resourcePath = resourcePath.Substring(resourcePath.LastIndexOf('/') + 1);
            
            Sprite itemicon = Array.Find(itemicons, x => x.name == resourcePath);
            
            //원래는 single sprite 라면 이런식으로 로드해야하긴해요~
            //Sprite itemicon = Managers.Resource.Load<Sprite>(itemInfo.resourcePath);
            
            Debug.Log("UI_Inventory_Slot" + itemicon);

            icon.sprite = itemicon;
        }
    }

}