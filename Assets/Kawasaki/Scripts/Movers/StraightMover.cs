using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 直線的に動くもの
    /// </summary>
    public class StraightMover : Mover
    {
        /// <summary>
        /// 移動方向
        /// </summary>
        [SerializeField]
        Vector2 _direction = Vector2.zero;

        private void FixedUpdate()
        {
            Vector3 velocity = _speed * _direction.normalized;
            Vector3 position = transform.position + velocity;
            _rigidbody2D.MovePosition(position);
        }
    }
}
