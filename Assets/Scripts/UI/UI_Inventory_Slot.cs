using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Isometric.Data;

namespace Isometric.UI
{

    public class UI_Inventory_Slot : UI_Base
    {
        private GraphicRaycaster gr;
        private int slotNum;
        public int SlotNum { get => slotNum; set => slotNum = value; }
        public Image icon;
        private bool hasItem = false;
        public bool HasItem { get { return hasItem; } set { hasItem = value; } }
        private void Start()
        {
            Init();
        }
        public override void Init()
        {
            gr = GetComponent<GraphicRaycaster>();
            RectTransform rectTransform = GetComponent<RectTransform>();
            
            
            //BindEvent
            BindEvent(this.gameObject, PointerEventData => IconDrag(), type:Enums.UIEvent.Drag);
            BindEvent(this.gameObject, PointerEventDAta => IconBeginDrag(), type: Enums.UIEvent.BeginDrag);
            BindEvent(this.gameObject, PointerEventData => IconEndDrag(), type: Enums.UIEvent.EndDrag);
            
        }

        public void IconBeginDrag()
        {
            CursorController.Instance.BeginDragSlot = slotNum;
        }
        public void IconEndDrag()
        {
            var ped = new PointerEventData(null);
            ped.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(ped, results);
            for(int i = 0; i < results.Count; i++)
            {
                Debug.Log(results[i].gameObject.name);
            }
            
            //�ƹ� UI���� �������� ���콺 ������
            if(results.Count <= 0)
            {
                return;
            }
            //��� �� ��� ° ���� ĭ�� ������� ���� �ޱ�

            //UI_Inventory_Slot newSlot = results[0].gameObject.GetComponent<UI_Inventory_Slot>();
            UI_Inventory_Slot newSlot = results[0].gameObject.GetComponentInParent<UI_Inventory_Slot>();
            Debug.Log("�巡�׾� ������� �ν��� �κ�Ʈ�� ������ ��ġ ǥ��" + newSlot.gameObject.transform.localPosition.x + " " + newSlot.gameObject.transform.localPosition.y + " " + newSlot.gameObject.transform.localPosition.z);
            Debug.Log("�巡�� �� ����� ��� ���� ::: " + newSlot.SlotNum);
            Debug.Log("�巡�� �� ����� ���� ���� ::: " + CursorController.Instance.BeginDragSlot);

            if (newSlot != null)
            {
                Debug.Log("������ �巡�׾� ��� �� �� �αװ� ���� ������ null CHeck ����");
                if (newSlot.HasItem)
                {
                    
                    Managers.Inven.Items[Managers.Inven.Find(x => x.Slot == newSlot.SlotNum).ItemDbId].Slot = CursorController.Instance.BeginDragSlot;
                    Managers.Inven.Items[Managers.Inven.Find(x => x.Slot == CursorController.Instance.BeginDragSlot).ItemDbId].Slot = newSlot.SlotNum;


                }
                else
                {
                    Managers.Inven.Items[Managers.Inven.Find(x => x.Slot == CursorController.Instance.BeginDragSlot).ItemDbId].Slot = newSlot.SlotNum;
                }

            }

        
            UI_Inventory.Instance.RefreshSlot();
        }
        public void IconDrag()
        {
            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
            CursorController.Instance.OnGrabbed(icon);
        }
        
        public void SetItem(int templateid, int count)
        {
            Data.ItemInfo itemInfo = null;
            Managers.Data.ItemInfoDict.TryGetValue(templateid, out itemInfo);

            Sprite[] itemicons = Resources.LoadAll<Sprite>("New/Test/Fantastic UI Starter Pack");
            Debug.Log(itemicons +"itemicon multiple sprite ���� �ε�" +itemicons.Length);
            Debug.Log(itemInfo.resourcePath);
            string resourcePath = itemInfo.resourcePath;
            resourcePath = resourcePath.Substring(resourcePath.LastIndexOf('/') + 1);
            
            Sprite itemicon = Array.Find(itemicons, x => x.name == resourcePath);
            
            //������ single sprite ��� �̷������� �ε��ؾ��ϱ��ؿ�~
            //Sprite itemicon = Managers.Resource.Load<Sprite>(itemInfo.resourcePath);
            
            Debug.Log("UI_Inventory_Slot" + itemicon);

            icon.sprite = itemicon;
        }
    }

}