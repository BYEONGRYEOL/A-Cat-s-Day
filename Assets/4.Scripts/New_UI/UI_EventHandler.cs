using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

namespace Isometric.UI
{
    public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IPointerClickHandler, IDragHandler
    {

        public Action<PointerEventData> OnClickHandler = null;
        public Action<PointerEventData> OnDragHandler = null;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("드래그 시작");
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (OnDragHandler != null)
            {
                OnDragHandler.Invoke(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (OnClickHandler != null)
            {
                OnClickHandler.Invoke(eventData);
            }
        }
        // Start is called before the first frame update


    }

}