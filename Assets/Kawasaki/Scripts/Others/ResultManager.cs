using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Kawasaki
{
    /// <summary>
    /// リザルトシーンの管理者
    /// </summary>
    public class ResultManager : MonoBehaviour
    {
        // 試合結果
        Tsuguhiko.ResultDisplay _resultDisplay = null;

        /// <summary>
        /// 勝利テキスト
        /// </summary>
        [SerializeField]
        TextMeshProUGUI _winText = null;

        /// <summary>
        /// 敗北テキスト
        /// </summary>
        [SerializeField]
        TextMeshProUGUI _loseText = null;

        private void Awake()
        {
            _resultDisplay = GetComponentInChildren<Tsuguhiko.ResultDisplay>(true);
        }

        private void Start()
        {
            // 試合結果
            GameResult gameResult = _resultDisplay.ResultJudge;
            if (gameResult.Win)
            {
                OnWin();
            }
            else
            {
                OnLose();
            }
        }

        /// <summary>
        /// 勝利時の処理
        /// </summary>
        private void OnWin()
        {
            _winText.enabled = true;
            _loseText.enabled = false;
        }

        /// <summary>
        /// 敗北時の処理
        /// </summary>
        private void OnLose()
        {
            _loseText.enabled = true;
            _winText.enabled = false;
        }
    }
}
