using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{

    public class CursorController : MonoBehaviour
    {
        int mask = (1 << (int)Enums.Layer.InterActive);
        // Start is called before the first frame update

        Texture2D idleIcon;
        Camera main;
        enum CursorType
        {
            Idle,
            Attack,
            Hand
        }
        CursorType cursorType = CursorType.Idle;
        void Start()
        {
            idleIcon = Managers.Resource.Load<Texture2D>("Texture/Cursor/Cursor_Idle");
            main = Camera.main;
            Debug.Log(main);
        }

        // Update is called once per frame
        void Update()
        {
            UpdateCursor();
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