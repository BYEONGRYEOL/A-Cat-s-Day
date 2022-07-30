using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Isometric.UI;
using Isometric.Data;
using UnityEngine.EventSystems;

using Isometric.Utility;
namespace Isometric
{
    public class GameManager
    {
        
        /*private static GameManager instance;
        public static GameManager Instance{get{return instance;}}
        private void OnDestroy(){if (instance == this){instance = null;}}
        */
        private GameObject myPlayer;


        public Vector3 clickPosition;


        private bool isGameOver;
        private bool isPaused;

        public bool IsGameOver { get { return isGameOver; } }
        public bool IsPaused { get => isPaused; }
        
        public void Init()
        {
            myPlayer = GameObject.FindGameObjectWithTag("Player");     
        }

        public void DropItem(int id, Vector2 startPosition, Vector2 endPosition)
        {
            // enemy.id 와 비교 구조물 트리거 등의 id와도 비교.
            float distance = Vector2.Distance(startPosition, endPosition);
            Vector2 midpoint = new Vector2((startPosition.x + endPosition.x) / 2, 
                (startPosition.y - endPosition.y) / 2 + startPosition.y);


            if (Managers.Data.EnemyStatDict.ContainsKey(id))
            {
                // 적이 죽어서 아이템을 떨궈야 하는 경우
                // 확률 등을 생각해서 아이템을 해당 위치에 떨궈야겠다.
            }
        }
        public void EndLevel()
        {
            
            
        }

        private void ClickTarget()
        {
            //마우스 좌클릭과 동시에 UI element를 클릭한 경우가 아닌경우에만
            if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector3.zero, Mathf.Infinity, 512);
                
                if(hit.collider != null)
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        clickPosition = hit.transform.position;
                    }
                }
            }
        }
        private void Start()
        {
            
        }
        // Update is called once per frame
        public void OnUpdate()
        {
            ClickTarget();
            SetUI();
        }
        
        public void SetUI()
        {
            
            UI_Game gameUI = Managers.UI.SceneUI as UI_Game;
            if(gameUI == null)
            {
                //캐스팅 실패 -> 현재 ui가 인게임ui가 아님을 이야기하는것
                return;
            }

            UI_Inventory inventory = gameUI.inventory;
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventory.isActiveAndEnabled)
                {
                    inventory.gameObject.SetActive(false);
                }
                else
                {
                    inventory.gameObject.SetActive(true);
                    inventory.RefreshSlot();
                }
            }
        }
        
        public void MyResume()
        {
            isPaused = false;
            Debug.Log("timeScale is" + Time.timeScale);
            
        }
       
    }
}