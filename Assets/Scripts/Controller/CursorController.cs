using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric.Utility;
using UnityEngine.UI;
namespace Isometric
{

    public class CursorController : SingletonMonoBehaviour<CursorController>
    {
        int mask = (1 << (int)Enums.Layer.InterActive);
        // Start is called before the first frame update
        Image grabbingImage;
        Texture2D idleIcon;
        Texture2D grabIcon;
        Camera main;

        public int BeginDragSlot;

        public bool IsGrabbing = false;
        enum CursorType
        {
            Idle,
            Attack,
            Grab
        }
        CursorType cursorType = CursorType.Idle;
        private void Start()
        {
            Init();
        }
        void Init()
        {
            idleIcon = Managers.Resource.Load<Texture2D>("Texture/Cursor/Cursor_Idle");
            grabIcon = Managers.Resource.Load<Texture2D>("Texture/Cursor/Cursor_Grab");
            main = Camera.main;
            
            IsGrabbing = false;
        }

        void Update()
        {
            UpdateCursor();
        }
        public void OnGrabbed(Image image)
        {
            //잡고있던 마우스를 떼면 리턴
            if (Input.GetMouseButtonUp(0))
                return;
            Cursor.SetCursor(grabIcon, new Vector2(grabIcon.width / 4, grabIcon.height / 4), CursorMode.Auto);
            grabbingImage = image;
            grabbingImage.transform.position = Input.mousePosition;
            
        }
        void UpdateCursor()
        {
            
            if (Input.GetMouseButton(0))
                return;


            Ray ray = main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f, mask))
            {

            }
            else
            {
                Cursor.SetCursor(idleIcon, new Vector2(idleIcon.width / 4, idleIcon.height / 4), CursorMode.Auto);
            }
        }
    }

}