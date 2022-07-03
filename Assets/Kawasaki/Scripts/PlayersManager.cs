using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤーの管理者
    /// </summary>
    public class PlayersManager : MonoBehaviour
    {
        /// <summary>
        /// 現在のシーンにおけるインスタンス
        /// </summary>
        public static PlayersManager Current = null;

        /// <summary>
        /// プレイヤーリスト
        /// </summary>
        readonly List<Player> _players = new();

        private void Awake()
        {
            Current = this;
        }

        public static void Register(Player player)
        {
            Current._players.Add(player);
        }

        private void LateUpdate()
        {
            // 最下位フラグを全てOFFにする
            foreach (var player in _players)
            {
                player.IsInTheLowestPosition = false;
            }

            // 最も低い位置にいるプレイヤーの最下位フラグをONにする
            Player lowest = _players
                .Where(x => x != null)
                .OrderBy(x => x.transform.position.y)
                .FirstOrDefault();
            
            if (lowest)
            {
                lowest.IsInTheLowestPosition = true;
            }
        }
    }

}
