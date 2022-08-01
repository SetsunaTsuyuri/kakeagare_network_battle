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
        /// 現在のシーンに存在するインスタンス
        /// </summary>
        public static PlayersManager Current { get; private set; } = null;

        /// <summary>
        /// プレイヤーリスト
        /// </summary>
        readonly List<Player> _players = new();

        private void Awake()
        {
            Current = this;
        }

        private void LateUpdate()
        {
            if (!_players.Any())
            {
                return;
            }

            // 最下位フラグを全てOFFにする
            foreach (var player in _players)
            {
                player.IsInTheLowestPosition = false;
            }

            // 高い位置にいる順に並んだプレイヤー配列
            Player[] ranking = _players
                .Where(x => x != null)
                .OrderByDescending(x => x.transform.position.y)
                .ToArray();

            // プレイヤーの順位を設定する
            for (int i = 0; i < ranking.Length; i++)
            {
                ranking[i].Rank = i + 1;
            }

            // 最も低い位置にいるプレイヤーの最下位フラグをONにする
            Player lowest = ranking.Last();
            if (lowest)
            {
                lowest.IsInTheLowestPosition = true;
            }

        }

        /// <summary>
        /// プレイヤーを登録する
        /// </summary>
        /// <param name="player">プレイヤー</param>
        public void Register(Player player)
        {
            _players.Add(player);
        }

        /// <summary>
        /// クライアントが操作するプレイヤーを取得する
        /// </summary>
        /// <returns></returns>
        public Player GetMyPlayer()
        {
            Player player = _players
                .FirstOrDefault(x => x.PhotonView.IsMine);

            return player;
        }
    }

}
