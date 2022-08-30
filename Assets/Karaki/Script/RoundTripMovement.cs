using UnityEngine;

namespace Karaki
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RoundTripMovement : MonoBehaviour
    {
        [SerializeField, Tooltip("移動力")]
        float _movePower = 5f;

        /// <summary>移動方向</summary>
        Vector2 _moveDirection;

        [SerializeField, Tooltip("タグ名 : 移動できる地面")]
        string _tagNameGround = "Ground";

        [SerializeField, Tooltip("タグ名 : プレイヤーの弾")]
        string _tagNameObject = "Bullet";

        /// <summary>地面に接触している回数</summary>
        int _stayGround = 0;

        /// <summary>剛体情報</summary>
        Rigidbody2D _rb = null;

        /// <summary>アニメーター</summary>
        Animator _animator = null;

        [SerializeField, Tooltip("壁の有無を確認するためのコンポーネント")]
        ObjectSeeker _objectSeekerForWall = null;

        [SerializeField, Tooltip("崖の有無を確認するためのコンポーネント")]
        ObjectSeeker _objectSeekerForCriff = null;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            //子オブジェクトにつけているコライダーのトリガーコールバックで実行するメソッドを登録
            if (_objectSeekerForWall) _objectSeekerForWall.OnObjectEnter = OnFindWallForward;
            if (_objectSeekerForCriff)
            {
                _objectSeekerForCriff.OnObjectEnter = OnFindGroundOnFoot;
                _objectSeekerForCriff.OnObjectExit = OnFindCriffOnFoot;
            }

            _rb.velocity = new Vector2(_movePower, 0f);

            _animator.SetBool("IsMove", true);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //この敵キャラクターが攻撃を受けた
            if (collision.gameObject.CompareTag(_tagNameObject))
            {
                if(_rb.velocity.sqrMagnitude > 0.01f)
                {
                    _moveDirection = new Vector2(_rb.velocity.x, 0f);
                    _rb.velocity = Vector2.zero;
                }
                _animator.SetTrigger("IsDamaged");
            }
        }

        /// <summary>ダメージリアクションの終了処理</summary>
        void DamageEnd()
        {
            _rb.velocity = _moveDirection;
        }

        /// <summary>正面に壁を見つけた時に実行するメソッド</summary>
        /// <param name="collision">見つけた壁情報</param>
        void OnFindWallForward(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameGround))
            {
                DoTurn();
            }
        }

        /// <summary>正面の足元に床を発見したときに実行するメソッド</summary>
        /// <param name="collision">発見した床情報</param>
        void OnFindGroundOnFoot(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameGround))
            {
                _stayGround++;
            }
        }

        /// <summary>正面の足元に床がないことを確認したときに実行するメソッド</summary>
        /// <param name="collision">見失った床情報</param>
        void OnFindCriffOnFoot(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(_tagNameGround))
            {
                _stayGround--;
                if (_stayGround < 1) DoTurn();
            }
        }


        /// <summary>向きを逆に</summary>
        void DoTurn()
        {
            transform.right *= -1f;
            _rb.velocity = new Vector2(-_rb.velocity.x, 0f);
        }
    }
}