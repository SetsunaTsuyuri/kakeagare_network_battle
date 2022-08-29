using System;
using UnityEngine;

public class ObjectSeeker : MonoBehaviour
{
    [SerializeField, Tooltip("タグ名 : 探すオブジェクトのタグ名")]
    string _tagNameSeekObject = "Ground";

    /// <summary>オブジェクトが触れた時に実行するメソッドを登録</summary>
    public Action<Collider2D> OnObjectEnter = null;

    /// <summary>オブジェクトが離れた時に実行するメソッドを登録</summary>
    public Action<Collider2D> OnObjectExit = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnObjectEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnObjectExit?.Invoke(collision);
    }
}
