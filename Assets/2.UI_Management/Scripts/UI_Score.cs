using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Isometric;
using TMPro;
using Isometric.Data;

using Isometric.Utility;

namespace Isometric.UI
{
    public class UI_Score : UI_Menu<UI_Score>
    {
        [SerializeField] private TextMeshProUGUI starComment;
        [SerializeField] private TextMeshProUGUI levelComment;

        public void OpenMenu()
        {
            Initialize();
        }
        private void Initialize()
        {

        }
        public void OnBtnAboutPressed()
        {
            UI_About.Open();
        }
        public override void OnBtnBackPressed()
        {

            base.OnBtnBackPressed();
            

        }

        public void OnBtnLeaderBoardPressed()
        {
            //GooglePlayManager.Instance.ShowLeaderBoard();
        }

        public void OnBtnAchievementsPressed()
        {
            //GooglePlayManager.Instance.ShowAchievement();
        }
    }
}