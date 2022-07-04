using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;

namespace Karaki
{
    /// <summary>
    /// �v���C���[�̓����𐧌䂷��R���|�[�l���g
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
    public class PlayerMovement : MonoBehaviour
    {
        /// <summary>�����ړ��̓��͖�</summary>
        const string _INPUT_NAME_HORIZONTAL = "Horizontal";

        /// <summary>�W�����v����̓��͖�</summary>
        const string _INPUT_NAME_JUMP = "Jump";

        /// <summary>�n�ʃ��C����</summary>
        const string _LAYER_NAME_GROUND = "Ground";

        /// <summary>�e���C����</summary>
        const string _LAYER_NAME_BULLET = "Bullet";

        [SerializeField, Tooltip("�v���C���[�̈ړ����x")] 
        float _speed = 5f;

        [SerializeField, Tooltip("�v���C���[�̃W�����v���x")]
        float _jumpSpeed = 5f;

        [SerializeField, Tooltip("�C�⎞��(s)")]
        float _stunTime = 2f;

        /// <summary>�C�⎞�Ԃ��J�E���g����^�C�}�[</summary>
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
            //�����̃v���C���[�I�u�W�F�N�g�łȂ���Ύ󂯕t���Ȃ�
            if (!_view.IsMine) return;

            //�C�⎞�Ԃ��c���Ă���΁A�J�E���g���������ŗ��E
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
                //�n�ʂɐڐG
                case _LAYER_NAME_GROUND:
                    _isGrounded = true;
                    break;
                //�G�e�ɐڐG
                case _LAYER_NAME_BULLET:
                    //�C���Ԃ�
                    _stunTimeCount = _stunTime;
                    break;

                default: break;
            }
        }

        /// <summary>
        /// Cinemachine Virtual Camera �� Follow �Ɏ������Z�b�g����
        /// </summary>
        void FindCamera()
        {
            var vcam = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
            vcam.Follow = transform;
        }
    }
}
