using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Isometric
{
    public class Enemy_HP : Status
    {
        [SerializeField]
        private float hp_max = 100;
        private bool isDamaged = false;
        private GameObject hpBar = null;
        private Vector3 location;

        public bool IsAlive { get { return MyCurrentValue > 0; } }
        // Start is called before the first frame update
        void Start()
        {
            base.Init(hp_max, hp_max);
        }
        private void Update()
        {
            if(hpBar != null && IsAlive)
            {
                HPBar_Location();
            }
        }

        public void HP_Changed(float changeValue)
        {
            Debug.Log("공격된거?");
            MyCurrentValue += changeValue;
            if(hpBar == null)
            {
                Debug.Log("hpbar 생성");
                hpBar = NGUIObjectPool.GetObject();
            }
            hpBar.GetComponent<Enemy_HP_UI>().HP_barSetValue(MyCurrentValue, MyMaxValue);
            if (!IsAlive)
            {
                Invoke("HP_Bar_Return_Pool", 1f);
            }

        }

        public void HP_Bar_Return_Pool()
        {
            NGUIObjectPool.ReturnObject(hpBar);
        }
        public void HPBar_Location()
        {
            location = new Vector3(transform.position.x, transform.position.y + 0.5f, 0);
            hpBar.transform.position = location;
        }
      
    }

}