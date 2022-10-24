using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Tsuguhiko
{
    public class VariousButtonBehavior : ButtonBase
    {
        [SerializeField] EventSystem _eventSystem;

        WaitForSeconds _wfs;

        [SerializeField, Header("シーン切り換え待機時間")] float _second;

        [SerializeField, Header("遷移シーン名")] string[] _sceneNames;

        void Awake()
        {
            _wfs = new WaitForSeconds(_second);
        }


        public void SceneChangeButton()
        {
            StartCoroutine(StandByTime());
        }

        public void TittleButton()
        {
            StartCoroutine(TittleBack());
        }

        public void ExplanationButton()
        {
            StartCoroutine(Explanation());
        }

        public void ExitButton()
        {
            Application.Quit();
            Debug.Log("quitted game");
        }

        IEnumerator StandByTime()
        {
            _eventSystem.enabled = false;
            yield return _wfs;
            SceneManager.LoadScene(_sceneNames[0]);
        }

        IEnumerator TittleBack()
        {
            _eventSystem.enabled = false;
            yield return _wfs;
            SceneManager.LoadScene(_sceneNames[1]);
        }

        IEnumerator Explanation()
        {
            _eventSystem.enabled = false;
            yield return _wfs;
            SceneManager.LoadScene(_sceneNames[1]);
        }
    }
}