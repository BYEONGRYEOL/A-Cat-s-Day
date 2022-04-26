using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null;
    public Action MouseAction = null;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!Input.anyKey)
        {
            return;
            if (KeyAction != null)
            {
                KeyAction.Invoke();
            } 
        }

        if(MouseAction != null)
        {
            MouseAction.Invoke();
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
