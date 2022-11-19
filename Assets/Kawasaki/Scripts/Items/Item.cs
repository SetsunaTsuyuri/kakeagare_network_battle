using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// アイテム
    /// </summary>
    public class Item : MonoBehaviour
    {
        /// <summary>
        /// 設定
        /// </summary>
        [field: SerializeField]
        public ItemsSettings Settings { get; private set; } = null;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            BeObtained(collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            BeObtained(collision.gameObject);
        }

        /// <summary>
        /// 獲得される
        /// </summary>
        /// <param name="other">接触したゲームオブジェクト</param>
        private void BeObtained(GameObject other)
        {
            if (other.TryGetComponent(out IItemObtainer itemObtainer))
            {
                itemObtainer.Obtain(this);
            }
        }
    }
}
