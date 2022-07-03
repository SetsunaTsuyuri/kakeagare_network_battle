using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// プレイヤー
    /// </summary>
    public class Player : MonoBehaviour
    {
        PhotonView _view = null;

        /// <summary>
        /// 他のプレイヤーより低い位置にいる
        /// </summary>
        public bool IsInTheLowestPosition { get; set; } = false;

        private void Awake()
        {
            _view = GetComponent<PhotonView>();
        }

        private void Start()
        {
            // 管理者に自身を登録する
            PlayersManager.Register(this);

            // マップとキルゾーンはマスタークライアント側で生成する
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            // 自分がログインした時はマップを生成する
            if (_view.IsMine)
            {
                MapsManager.Current.CreateMaps();
            }
            else  // 2人目が入ってきた時にキルゾーンを生成する
            {
                MapsManager.Current.CreateKillZone();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IPlayerHit hit = collision.GetComponent<IPlayerHit>();
            if (hit is not null)
            {
                hit.OnHit(this);
            }
        }
    }
}
