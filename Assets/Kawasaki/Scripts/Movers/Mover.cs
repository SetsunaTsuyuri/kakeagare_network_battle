using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Kawasaki
{
    /// <summary>
    /// 動くもの
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Mover : MonoBehaviour
    {
        /// <summary>
        /// 速度
        /// </summary>
        [SerializeField]
        protected float _speed = 0.0f;

        /// <summary>
        /// 速度
        /// </summary>
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        /// <summary>
        /// リジッドボディ2D
        /// </summary>
        protected Rigidbody2D _rigidbody2D = null;

        /// <summary>
        /// フォトンビュー
        /// </summary>
        protected PhotonView _photonView = null;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _photonView = GetComponent<PhotonView>();
        }

        private void FixedUpdate()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                return;
            }

            Move();
        }

        /// <summary>
        /// 移動する
        /// </summary>
        protected abstract void Move();
    }
}
