using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isometric.UI
{
    [RequireComponent(typeof(ScreenFader))]
    public class LoadingScreenFirst : MonoBehaviour
    {
        [SerializeField] private ScreenFader screenFader;
        [SerializeField] private float delay = 1f;

        private void Awake()
        {
            screenFader = GetComponent<ScreenFader>();
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            screenFader.FadeOn();
        }
        public void FadeAndLoad()
        {
            StartCoroutine("FadeAndLoad_Coroutine");
        }
        private IEnumerator FadeAndLoad_Coroutine()
        {
            
            yield return new WaitForSeconds(delay);
            
            screenFader.FadeOff();
            LevelLoader.LoadMainMenu();
            yield return new WaitForSeconds(screenFader.FadeOffDuration);
            
            Destroy(gameObject);
            
        }
    }
}