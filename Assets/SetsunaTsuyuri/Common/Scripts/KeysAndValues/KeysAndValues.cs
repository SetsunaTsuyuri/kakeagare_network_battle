using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// キーと値の組み合わせの集合体
    /// </summary>
    /// <typeparam name="TKey">キーの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    [Serializable]
    public class KeysAndValues<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// 規定値
        /// </summary>
        [SerializeField]
        TValue defaultValue;

        /// <summary>
        /// キーと値の組み合わせ配列
        /// </summary>
        [SerializeField]
        KeyAndValue<TKey, TValue>[] data;
        
        /// <summary>
        /// 値または規定値を取得する
        /// </summary>
        /// <param name="key">キー</param>
        /// <returns></returns>
        public TValue GetValueOrDefault(TKey key)
        {
            TValue result = defaultValue;

            if (data.Any())
            {
                KeyAndValue<TKey, TValue> keyAndValue = data.FirstOrDefault(x => x.Key.CompareTo(key) == 0);
                if (keyAndValue != null)
                {
                    result = keyAndValue.Value;
                }
            }

            return result;
        }
    }
}
