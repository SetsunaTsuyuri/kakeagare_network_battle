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
        /// <summary>
        /// フォトンビュー
        /// </summary>
        public PhotonView PhotonView { get; private set; } = null;

        /// <summary>
        /// 他のプレイヤーより低い位置にいる
        /// </summary>
        public bool IsInTheLowestPosition { get; set; } = false;

        /// <summary>
        /// プレイヤーの移動
        /// </summary>
        Karaki.PlayerMovement _movement = null;

        /// <summary>
        /// 初期の回転(Y軸)
        /// </summary>
        float _defaultRotationY = 0.0f;

        private void Awake()
        {
            PhotonView = GetComponent<PhotonView>();
            _movement = GetComponent<Karaki.PlayerMovement>();
            _defaultRotationY = transform.rotation.y;
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
            if (PhotonView.IsMine)
            {
                MapsManager.Current.CreateMaps();
            }
            else  // 2人目が入ってきた時にキルゾーンを生成する
            {
                MapsManager.Current.CreateKillZone();
            }
        }

        private void LateUpdate()
        {
            if (!PhotonView.IsMine)
            {
                return;
            }

            // 回転を更新する
            UpdateRotation();
        }

        /// <summary>
        /// 回転を更新する
        /// </summary>
        private void UpdateRotation()
        {
            // 左右の入力によって回転角度を変える
            float axis = Input.GetAxisRaw("Horizontal");
            if (axis != 0.0f)
            {
                Quaternion newRotation = transform.rotation;
                if (axis > 0.0f)
                {
                    newRotation.y = _defaultRotationY;
                }
                else if (axis < 0.0f)
                {
                    newRotation.y = _defaultRotationY + 180.0f;
                }
                transform.rotation = newRotation;
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

        /// <summary>
        /// 加速する
        /// </summary>
        /// <param name="scale">加速倍率</param>
        /// <param name="time">効果時間</param>
        public void Accelerate(float scale, float time)
        {
            _movement.Accelerate(scale, time);
        }

        /// <summary>
        /// 気絶する
        /// </summary>
        /// <param name="time">効果時間</param>
        public void BeStunned(float time)
        {
            _movement.BeStunned(time);
        }
    }
}
