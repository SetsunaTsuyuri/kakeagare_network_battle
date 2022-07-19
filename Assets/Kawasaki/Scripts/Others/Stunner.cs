using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤーを気絶させるもの
    /// </summary>
    public class Stunner : MonoBehaviour
    {
        /// <summary>
        /// 気絶させる時間
        /// </summary>
        [SerializeField]
        float duration = 0.0f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Stun(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Stun(collision.gameObject);
        }

        /// <summary>
        /// 気絶させる
        /// </summary>
        /// <param name="other">接触したゲームオブジェクト</param>
        private void Stun(GameObject other)
        {
            IStunned stunned = other.GetComponentInParent<IStunned>();
            if (stunned is not null)
            {
                stunned.BeStunned(duration);
            }
        }
    }
}
