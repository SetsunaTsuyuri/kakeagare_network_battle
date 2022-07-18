using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転するもの
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Rotator : MonoBehaviour
{
    /// <summary>
    /// 回転速度
    /// </summary>
    [SerializeField]
    float _speed = 0.0f;

    /// <summary>
    /// リジッドボディ2D
    /// </summary>
    Rigidbody2D _rigidbody2D = null;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float angle = _rigidbody2D.rotation + _speed;
        angle = Mathf.Repeat(angle, 360.0f);
        _rigidbody2D.MoveRotation(angle);
    }
}
