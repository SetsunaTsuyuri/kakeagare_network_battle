using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 揺れるもの
    /// </summary>
    public class Swinger : Mover
    {
        /// <summary>
        /// 移動先
        /// </summary>
        [SerializeField]
        Vector2 _destination = Vector2.zero;

        /// 初期位置
        /// </summary>
        Vector2 _initialPosition = Vector2.zero;

        /// <summary>
        /// 実際の速度
        /// </summary>
        float _actualSpeed = 0.0f;

        protected override void Awake()
        {
            base.Awake();

            _initialPosition = transform.position;

            float magnitude = _destination.magnitude;
            if (magnitude > 0.0f)
            {
                _actualSpeed = _speed / magnitude;
            }
        }

        protected override void Move()
        {
            Vector2 position = _initialPosition;
            position.x += _destination.x * Mathf.Sin(_actualSpeed * Time.time);
            position.y += _destination.y * Mathf.Sin(_actualSpeed * Time.time);

            _rigidbody2D.MovePosition(position);
        }
    }
}
