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
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
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

            // アニメーターパラメーターを更新する
            UpdateAnimatorParameters();
        }

        /// <summary>
        /// アニメーターパラメーターを更新する
        /// </summary>
        private void UpdateAnimatorParameters()
        {
            float inputHorizontal = Mathf.Abs(Input.GetAxis("Horizontal"));
            _animator.SetFloat("Speed", inputHorizontal);

            float velocityY = _rigidbody2D.velocity.y;
            _animator.SetFloat("VelocityY", velocityY);

            float groundFromDistance = 0.0f;
            RaycastHit2D hit = Physics2D.Raycast(_foot.position, Vector2.down);
            if (hit.collider != null)
            {
                groundFromDistance = hit.distance;
            }

            _animator.SetFloat("GroundFromDistance", groundFromDistance);
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
