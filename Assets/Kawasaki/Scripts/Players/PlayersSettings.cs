using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤーの設定
    /// </summary>
    [CreateAssetMenu(fileName = "Players", menuName = ("Kawasaki/Settings/Players"))]
    public class PlayersSettings : ScriptableObject
    {
        /// <summary>
        /// キャスト結果配列の要素数
        /// </summary>
        [field: SerializeField]
        public int CastingResultsLength { get; private set; } = 5;
    }
}
