using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// キーと値のデータ集
    /// </summary>
    public class KeysAndValuesCollection<TKey, TValue> : ScriptableObject
        where TKey : System.IComparable
    {
        /// <summary>
        /// キーと値
        /// </summary>
        [SerializeField]
        KeysAndValues<TKey, TValue> keysAndValues = null; 

        /// <summary>
        /// 値を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public TValue GetValueOrDefault(TKey key)
        {
            return keysAndValues.GetValueOrDefault(key);
        }
    }
}
