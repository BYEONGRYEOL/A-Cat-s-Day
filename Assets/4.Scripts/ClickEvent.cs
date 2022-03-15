using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public Camera cam;
    RaycastHit hit;
    
    public float _maxRayDistance = 1.5f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Cursor.visible)
            {
                Cursor.visible = false;
            }
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,  0.5f, 0));
            
            if(Physics.Raycast(ray, out hit, _maxRayDistance))
            {
                Debug.Log("Ray�� ������ ��Ҵ�.");
                if (hit.transform.CompareTag("Bed"))
                {
                    
                }
                else if (hit.transform.CompareTag("Sink"))
                {
                    Debug.Log("��ũ�� �ν�");

                    
                    //hit.transform.gameObject.GetComponent<Sink>().SinkAnimation();
                }
            }
           
        }
    }

}
