using UnityEngine;
using Cinemachine;
using Photon.Pun;

namespace Karaki
{
    /// <summary>
    /// プレイヤーの動きを制御するコンポーネント
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class PlayerMovement : MonoBehaviour
    {
        #region メンバ
        [SerializeField, Tooltip("プレイヤーの移動速度")] 
        float _speed = 5f;

        [SerializeField, Tooltip("プレイヤーのジャンプ速度")]
        float _jumpSpeed = 5f;

        [SerializeField, Tooltip("気絶時間(s)")]
        float _stunTime = 2f;

        /// <summary>気絶時間をカウントするタイマー</summary>
        float _stunTimeCount = 0f;

        /// <summary>移動速度UP倍率</summary>
        float _speedUpRate = 1f;

        /// <summary>移動速度UP時間をカウントするタイマー</summary>
        float _speedUpTimeCount = 0f;

        PhotonView _view;
        Rigidbody2D _rb;

        /// <summary>true : 地面オブジェクトに接触している</summary>
        bool _isGrounded = false;

        #endregion


        #region プロパティ

        /// <summary>気絶時間をカウントするタイマー</summary>
        public float StunTimeCount { get => _stunTimeCount; }

        /// <summary>移動速度UP時間をカウントするタイマー</summary>
        public float SpeedUpTimeCount { get => _speedUpTimeCount; }

        /// <summary>true : 地面オブジェクトに接触している</summary>
        public bool IsGrounded { get => _isGrounded; set => _isGrounded = value; }

        #endregion

        private void Start()
        {
            _view = gameObject.GetPhotonView();
            _rb = GetComponent<Rigidbody2D>();

            if (_view.IsMine)
            {
                FindCamera();
            }
        }

        /// <summary>プレイヤーキャラクターの移動を実施</summary>
        /// <param name="horizontalMoveRate">水平移動入力値</param>
        /// <param name="doJumpUp">ジャンプ用ボタンが押されたフラグ</param>
        public void Move(float horizontalMoveRate, bool doJumpUp)
        {
            //自分のプレイヤーオブジェクトでなければ受け付けない
            if (!_view.IsMine) return;

            //気絶時間が残っていれば、カウントしたうえで離脱
            if (_stunTimeCount > 0f)
            {
                _stunTimeCount -= Time.deltaTime;
                return;
            }

            //移動速度UP時間が残っていれば時間経過
            if (_speedUpTimeCount > 0f)
            {
                _speedUpTimeCount -= Time.deltaTime;
                //今の時間経過で0になったら速度倍率を1に戻す
                if(_speedUpTimeCount < 0f)
                {
                    _speedUpRate = 1f;
                }
            }

            //横移動
            Vector2 velocity = _rb.velocity;
            velocity.x = _speed * horizontalMoveRate * _speedUpRate;

            //ジャンプ動作
            if (doJumpUp && _isGrounded)
            {
                velocity.y = _jumpSpeed;
                _isGrounded = false;
            }

            //移動量を適用
            _rb.velocity = velocity;
        }
                
        /// <summary>Cinemachine Virtual Camera の Follow に自分をセットする</summary>
        void FindCamera()
        {
            var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = transform;
        }

        /// <summary>(time)秒間、移動速度が(scale)倍になる。(time)秒間経過後、元の移動速度に戻る</summary>
        /// <param name="scale">速度倍数</param>
        /// <param name="time">速度倍数がかかり続ける時間(s)</param>
        public void Accelerate(float scale, float time)
        {
            //速度倍率設定
            _speedUpRate = scale;
            _speedUpTimeCount = time;
        }

        /// <summary>(time) 秒間、操作不能になる</summary>
        /// <param name="time">気絶時間(s)</param>
        public void BeStunned(float time)
        {
            //気絶状態に
            _stunTimeCount = time;
        }

    }
}
