using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Isometric
{

    public class SkillIconClick : MonoBehaviour, IPointerClickHandler
    {
        
        [SerializeField] private string spellName;
        private Image icon;
        public Image Icon { get => icon; set => icon = value; }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button == PointerEventData.InputButton.Left)
            {
                MouseScript.Instance.TakeMoveable(UseableCollections.Instance.GetSpell(spellName));
            }
            if(eventData.button == PointerEventData.InputButton.Right)
            {
                UseableFunctions.Instance.Function(spellName);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Icon.sprite = UseableCollections.Instance.GetSpell(spellName).MyIcon;
            Icon.color = Color.white;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}