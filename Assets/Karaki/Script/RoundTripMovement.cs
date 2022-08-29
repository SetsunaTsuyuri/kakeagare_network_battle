using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karaki
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RoundTripMovement : MonoBehaviour
    {
        [SerializeField, Tooltip("移動力")]
        float _movePower = 5f;

        [SerializeField, Tooltip("タグ名 : 移動できる地面")]
        string _tagNameGround = "Ground";

        /// <summary>地面に接触している回数</summary>
        int _stayGround = 0;

        /// <summary>剛体情報</summary>
        Rigidbody2D _rb = null;

        [SerializeField, Tooltip("壁の有無を確認するためのコンポーネント")]
        ObjectSeeker _objectSeekerForWall = null;

        [SerializeField, Tooltip("崖の有無を確認するためのコンポーネント")]
        ObjectSeeker _objectSeekerForCriff = null;

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            //子オブジェクトにつけているコライダーのトリガーコールバックで実行するメソッドを登録
            if (_objectSeekerForWall) _objectSeekerForWall.OnObjectEnter = OnFindWallForward;
            if (_objectSeekerForCriff)
            {
                _objectSeekerForCriff.OnObjectEnter = OnFindGroundOnFoot;
                _objectSeekerForCriff.OnObjectExit = OnFindCriffOnFoot;
            }
            
            _rb.velocity = new Vector2(_movePower, 0f);
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
                if(_stayGround < 1) DoTurn();
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