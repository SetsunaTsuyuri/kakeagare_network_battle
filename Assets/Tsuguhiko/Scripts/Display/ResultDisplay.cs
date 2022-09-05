using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>リザルトシーンで勝敗を表示させるためのスクリプト</summary>
namespace Tsuguhiko
{
    public class ResultDisplay : MonoBehaviour
    {
        Kawasaki.GameResult _resultJudge;

        public Kawasaki.GameResult ResultJudge 
        {
            get 
            {
               return _resultJudge; 
            }

            set
            {
               _resultJudge = value;
            } 
        }
    }
}
