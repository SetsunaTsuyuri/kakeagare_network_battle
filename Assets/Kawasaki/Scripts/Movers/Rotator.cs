using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 回転するもの
    /// </summary>
    public class Rotator : Mover
    {
        private void FixedUpdate()
        {
            float angle = _rigidbody2D.rotation + _speed;
            angle = Mathf.Repeat(angle, 360.0f);
            _rigidbody2D.MoveRotation(angle);
        }
    }
}
