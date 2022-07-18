using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// アイテム
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Item : MonoBehaviour, IPlayerHit
    {
        /// <summary>
        /// 設定
        /// </summary>
        [SerializeField]
        ItemsSettings _settings = null;

        public void OnHit(Player player)
        {
            // 最も低い位置にいるプレイヤーの場合
            if (player.IsInTheLowestPosition)
            {
                // 有利な効果を適用する
                ApplyGoodEffect(player);
            }
            else
            {
                // 不利な効果を適用する
                ApplyBadEffect(player);
            }

            // 自身を破壊する
            Destroy(gameObject);
        }

        /// <summary>
        /// プレイヤーに良い効果を与える
        /// </summary>
        /// <param name="player"></param>
        private void ApplyGoodEffect(Player player)
        {
            Accelerate(player);
        }

        /// <summary>
        /// プレイヤーに悪い効果を与える
        /// </summary>
        /// <param name="player"></param>
        private void ApplyBadEffect(Player player)
        {
            // 気絶させる
            Stun(player);
        }

        /// <summary>
        /// 加速させる
        /// </summary>
        /// <param name="player">プレイヤー</param>
        private void Accelerate(Player player)
        {
            // テスト
            Debug.Log($"{player}は{_settings.AccelerationTime}秒間加速する");
            player.GetComponent<SpriteRenderer>().color = Color.blue;

            float scale = _settings.AccelerationScale;
            float time = _settings.AccelerationTime;
            player.Accelerate(scale, time);
        }

        /// <summary>
        /// 気絶させる
        /// </summary>
        /// <param name="player">プレイヤー</param>
        private void Stun(Player player)
        {
            // テスト
            Debug.Log($"{player}は{_settings.StunnedTime}秒間動けない");
            player.GetComponent<SpriteRenderer>().color = Color.red;

            float time = _settings.StunnedTime;
            player.BeStunned(time);
        }
    }
}
