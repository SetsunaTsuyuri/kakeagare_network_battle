using UnityEngine;
using Cinemachine;
using Photon.Pun;

/// <summary>
/// プレイヤーの動きを制御するコンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D), typeof(PhotonView))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    [SerializeField] float _jumpSpeed = 5f;
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
        if (!_view.IsMine) return;
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = _rb.velocity;
        velocity.x = _speed * h;

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = _jumpSpeed;
            _isGrounded = false;
        }

        _rb.velocity = velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = true;
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