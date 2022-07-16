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
        /// 足元のトランスフォーム
        /// </summary>
        [SerializeField]
        Transform _foot = null;

        /// <summary>
        /// フォトンビュー
        /// </summary>
        public PhotonView PhotonView { get; private set; } = null;

        /// <summary>
        /// 他のプレイヤーより低い位置にいる
        /// </summary>
        public bool IsInTheLowestPosition { get; set; } = false;

        /// <summary>
        /// リジッドボディ2D
        /// </summary>
        Rigidbody2D _rigidbody2D = null;

        /// <summary>
        /// アニメーター
        /// </summary>
        Animator _animator = null;

        /// <summary>
        /// プレイヤーの移動制御
        /// </summary>
        Karaki.PlayerMovement _movement = null;

        /// <summary>
        /// バレットランチャー
        /// </summary>
        Karaki.BulletLauncher _bulletLauncher = null;

        /// <summary>
        /// 初期の回転(Y軸)
        /// </summary>
        float _defaultRotationY = 0.0f;

        /// <summary>
        /// 仮想軸(水平)の入力
        /// </summary>
        float _horizontalAxisInput = 0.0f;

        /// <summary>
        /// ジャンプの入力
        /// </summary>
        bool _jumpInput = false;

        /// <summary>
        /// 射撃の入力
        /// </summary>
        bool _fireInput = false;

        private void Awake()
        {
            PhotonView = GetComponent<PhotonView>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _movement = GetComponent<Karaki.PlayerMovement>();
            _bulletLauncher = GetComponent<Karaki.BulletLauncher>();
            _defaultRotationY = transform.rotation.y;
        }

        private void Start()
        {
            // 管理者に自身を登録する
            PlayersManager.Current.Register(this);

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

        private void Update()
        {
            if (!PhotonView.IsMine)
            {
                return;
            }

            // 入力の更新処理
            UpdateInput();

            // 移動の更新処理
            UpdateMove();

            // 射撃の更新処理
            UpdateFire();

            // アニメーターパラメーターを更新する
            UpdateAnimatorParameters();
        }

        /// <summary>
        /// 入力の更新処理
        /// </summary>
        private void UpdateInput()
        {
            // 仮想軸(水平)
            _horizontalAxisInput = Input.GetAxisRaw("Horizontal");

            // ジャンプ
            _jumpInput = Input.GetButtonDown("Jump");

            // 射撃
            _fireInput = Input.GetButtonDown("Fire1");
        }

        /// <summary>
        /// 移動の更新処理
        /// </summary>
        private void UpdateMove()
        {
            //_movement.Move(_horizontalAxisInput, _jumpInput);
            _movement.SetRotationY(_defaultRotationY, _horizontalAxisInput);
        }

        /// <summary>
        /// 射撃の更新処理
        /// </summary>
        private void UpdateFire()
        {
            if (_fireInput)
            {
                //_bulletLauncher.Fire();
            }
        }

        /// <summary>
        /// アニメーターパラメーターを更新する
        /// </summary>
        private void UpdateAnimatorParameters()
        {
            // 軸(水平)の絶対値
            float horizontalAxisAbsolute = Mathf.Abs(_horizontalAxisInput);
            _animator.SetFloat("HorizontalAxisAbsolute", horizontalAxisAbsolute);

            // Y軸速度
            float velocityY = _rigidbody2D.velocity.y;
            _animator.SetFloat("VelocityY", velocityY);

            // 地上距離
            RaycastHit2D hit = Physics2D.Raycast(_foot.position, Vector2.down);
            float groundDistance = hit.distance;
            _animator.SetFloat("GroundDistance", groundDistance);

            // 気絶時間
            //float stunned = _movement.StunTimeCount;
            float stunned = 0.0f;
            _animator.SetFloat("Stunned", stunned);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IPlayerHit other = collision.GetComponent<IPlayerHit>();
            if (other is not null)
            {
                other.OnHit(this);
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
