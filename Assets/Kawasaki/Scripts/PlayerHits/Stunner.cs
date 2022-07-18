using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤーを気絶させるもの
    /// </summary>
    public class Stunner : MonoBehaviour, IPlayerHit
    {
        /// <summary>
        /// 気絶させる時間
        /// </summary>
        [SerializeField]
        float duration = 0.0f;

        public void OnHit(Player player)
        {
            player.BeStunned(duration);
        }
    }
}
