using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using Isometric.Data;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;


namespace Isometric.UI
{
    public class UI_Ingame_R : UI_Menu<UI_Ingame_R>
    {
        [SerializeField] private CanvasGroup skillCanvas;
        [SerializeField] private CanvasGroup ex1;
        [SerializeField] private CanvasGroup ex2;

        Player_HP hp;
        #region SerializeField
        [SerializeField] private Image hp_bar;
        [SerializeField] private TextMeshProUGUI hp_Text;
        #endregion

        [SerializeField] private ActionButton[] actionButtons;

        private float currentFill;
        private float lerpSpeed = 3f;

        private bool isStarted = false;

        private bool seeTutorial = true;
        private int tutorialStep = 0;
        private bool isBtnTutorialActivate = true;

        public bool IsStarted => isStarted;
        public float delay = 0.4f;
        public float myTimeScale = 1f;
        protected override void Awake()
        {
            base.Awake();
            
        }
        //Canvas Group �� �ϳ��� Ȱ��ȭ �Ѵ� = �� alpha���� 1�� blocksRaycasts ���� true��
        public void Canvas_Changer(CanvasGroup canvasGroup)
        {
            Debug.Log(canvasGroup + "â ����");
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
            canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true;

        }
        private void Update()
        {
            if(currentFill != hp_bar.fillAmount)
            {
                hp_bar.fillAmount = Mathf.Lerp(hp_bar.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
            }
            if(Input.GetKeyDown(KeyCode.K))
            {
                Canvas_Changer(skillCanvas);
            }
            // ���� �� ESC Ű�� �ɼ�â �� �� �־�
            // �̰� �ƿ� �ٸ� �������� ������ �ϴ� �Լ�
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.MyPause();
                UI_Option.Open();
            }
            
        }
        public void OpenMenu()
        {
            Debug.Log("Script :: UI_Ingame_R / Function :: OnEnable");
            hp = GetComponent<Player_HP>();
            Invoke("SceneInitialized", 1);
        }   
        public void SceneInitialized()
        {
            SetUsable(actionButtons[0], UseableCollections.Instance.GetSpell("Teleport"));
            SetUsable(actionButtons[1], UseableCollections.Instance.GetSpell("ACTION_2"));
            SetUsable(actionButtons[2], UseableCollections.Instance.GetSpell("ACTION_3"));
            Debug.Log(UseableCollections.Instance.GetSpell("ACTION_1"));
            Debug.Log("Script :: UI_Ingame_R / Function :: SceneInitialized");
            Managers.KeyBind.Init();
            
        }
        public void HP_barSetValue(float currentValue, float maxValue)
        {
            currentFill = currentValue / maxValue;
            hp_Text.text = currentValue + "/" + maxValue;
        }
        
        public void SetUsable(ActionButton btn, IUseable useable)
        {
            Debug.Log(btn + "��ư �̸�" + useable + "��� �����Ұ�");
            Debug.Log(btn.Icon.sprite);
            Debug.Log(useable.MyIcon);
            btn.Icon.sprite = useable.MyIcon;
            btn.Icon.color = Color.white;
            btn.MyUseable = useable;
        }

        public void ClickActionButton(string btnName)
        {
            Debug.Log(Array.Find(actionButtons, x => x.gameObject.name == btnName).MyButton.onClick);
            Debug.Log(Array.Find(actionButtons, x => x.gameObject.name == btnName).MyButton);
            Array.Find(actionButtons, x => x.gameObject.name == btnName).MyButton.onClick.Invoke();
        }
    }

}
