using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// アイテムの設定
    /// </summary>
    [CreateAssetMenu(fileName = "Items", menuName =("Kawasaki/Settings/Items"))]
    public class ItemSettings : ScriptableObject
    {
        /// <summary>
        /// 加速時間
        /// </summary>
        [field: SerializeField]
        public float AccelerationTime { get; private set; } = 5.0f;

        /// <summary>
        /// 加速倍率
        /// </summary>
        [field: SerializeField]
        public float AccelerationScale { get; private set; } = 1.5f;

        /// <summary>
        /// 気絶時間
        /// </summary>
        [field: SerializeField]
        public float StunningTime { get; private set; } = 1.0f;
    }
}
