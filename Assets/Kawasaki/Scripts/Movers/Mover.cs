using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawasaki
{
    /// <summary>
    /// 動くもの
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Mover : MonoBehaviour
    {
        /// <summary>
        /// 速度
        /// </summary>
        [SerializeField]
        protected float _speed = 0.0f;

        /// <summary>
        /// リジッドボディ2D
        /// </summary>
        protected Rigidbody2D _rigidbody2D = null;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}
