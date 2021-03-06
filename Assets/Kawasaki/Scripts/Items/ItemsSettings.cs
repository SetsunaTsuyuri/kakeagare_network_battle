using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// アイテムの設定
    /// </summary>
    [CreateAssetMenu(fileName = "Items", menuName =("Kawasaki/Settings/Items"))]
    public class ItemsSettings : ScriptableObject
    {
        /// <summary>
        /// 加速時間
        /// </summary>
        [field: SerializeField]
        public float AccelerationDuration { get; private set; } = 5.0f;

        /// <summary>
        /// 加速倍率
        /// </summary>
        [field: SerializeField]
        public float AccelerationScale { get; private set; } = 1.5f;

        /// <summary>
        /// 気絶時間
        /// </summary>
        [field: SerializeField]
        public float StunnedDutation { get; private set; } = 1.0f;
    }
}
