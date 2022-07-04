using System.Collections;
using System.Collections.Generic;
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
        /// <summary>水平移動の入力名</summary>
        const string _INPUT_NAME_HORIZONTAL = "Horizontal";

        /// <summary>ジャンプ操作の入力名</summary>
        const string _INPUT_NAME_JUMP = "Jump";

        /// <summary>地面レイヤ名</summary>
        const string _LAYER_NAME_GROUND = "Ground";

        /// <summary>弾レイヤ名</summary>
        const string _LAYER_NAME_BULLET = "Bullet";

        [SerializeField, Tooltip("プレイヤーの移動速度")] 
        float _speed = 5f;

        [SerializeField, Tooltip("プレイヤーのジャンプ速度")]
        float _jumpSpeed = 5f;

        [SerializeField, Tooltip("気絶時間(s)")]
        float _stunTime = 2f;

        /// <summary>気絶時間をカウントするタイマー</summary>
        float _stunTimeCount = 0f;

        PhotonView _view;
        Rigidbody2D _rb;
        bool _isGrounded = false;

        private void Start()
        {
            _view = gameObject.GetPhotonView();
            _rb = GetComponent<Rigidbody2D>();

            if (_view.IsMine)
            {
                FindCamera();
            }
        }

        private void Update()
        {
            //自分のプレイヤーオブジェクトでなければ受け付けない
            if (!_view.IsMine) return;

            //気絶時間が残っていれば、カウントしたうえで離脱
            if (_stunTimeCount > 0f)
            {
                _stunTimeCount -= Time.deltaTime;
                return;
            }

            float h = Input.GetAxisRaw(_INPUT_NAME_HORIZONTAL);
            Vector2 velocity = _rb.velocity;
            velocity.x = _speed * h;

            if (Input.GetButtonDown(_INPUT_NAME_JUMP) && _isGrounded)
            {
                velocity.y = _jumpSpeed;
                _isGrounded = false;
            }

            _rb.velocity = velocity;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.gameObject.tag)
            {
                //地面に接触
                case _LAYER_NAME_GROUND:
                    _isGrounded = true;
                    break;
                //敵弾に接触
                case _LAYER_NAME_BULLET:
                    //気絶状態に
                    _stunTimeCount = _stunTime;
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Cinemachine Virtual Camera の Follow に自分をセットする
        /// </summary>
        void FindCamera()
        {
            var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = transform;
        }
    }
}
