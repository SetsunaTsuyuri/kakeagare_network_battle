using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 巡回するもの
    /// </summary>
    public class Patroller : Mover
    {
        /// <summary>
        /// 目的地配列
        /// </summary>
        [SerializeField]
        Vector3[] destinations = { };

        /// <summary>
        /// 目的地の索引
        /// </summary>
        int _destinationIndex = 0;

        /// <summary>
        /// 初期位置
        /// </summary>
        Vector3 _initialPosition = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();

            _initialPosition = transform.position;
        }

        protected override void Move()
        {
            if (destinations.Length == 0)
            {
                return;
            }

            // 目的地
            Vector3 destination = _initialPosition + destinations[_destinationIndex];

            // 目的地までの距離を2乗した数
            float distance = (transform.position - destination).sqrMagnitude;

            if (distance >= 0.01f)
            {
                // 位置
                Vector2 position = Vector2.MoveTowards(transform.position, destination, _speed);

                // 移動する
                _rigidbody2D.MovePosition(position);
            }
            else
            {
                // 目的地を更新する
                _destinationIndex = (_destinationIndex + 1) % destinations.Length;
            }
        }
    }
}
