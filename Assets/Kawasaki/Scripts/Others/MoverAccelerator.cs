using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 動くものを加速させるもの
    /// </summary>
    [RequireComponent(typeof(Mover))]
    public class MoverAccelerator : MonoBehaviour
    {
        /// <summary>
        /// 加速
        /// </summary>
        [SerializeField]
        float _acceleration = 0.0f;

        /// <summary>
        /// 加速する間隔
        /// </summary>
        [SerializeField]
        float _interval = 0.0f;

        float _timer = 0.0f;

        /// <summary>
        /// 動くもの
        /// </summary>
        Mover _mover = null;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void FixedUpdate()
        {
            if (_timer >= _interval)
            {
                _timer = 0.0f;
                _mover.Speed += _acceleration;
            }
            else
            {
                _timer += Time.fixedDeltaTime;
            }
        }
    }
}
