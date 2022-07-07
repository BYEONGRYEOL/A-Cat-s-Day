using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.UI
{

    public class UI_Inventory_Slot : UI_Base
    {
        private bool hasItem = false;
        public bool HasItem { get { return hasItem; } set { hasItem = value; } }
    }

}