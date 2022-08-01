using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// キーと値の組み合わせ
    /// </summary>
    /// <typeparam name="TKey">キーの型</typeparam>
    /// <typeparam name="TValue">値の型</typeparam>
    [Serializable]
    public class KeyAndValue<TKey, TValue> where TKey : IComparable
    {
        /// <summary>
        /// キー
        /// </summary>
        [field: SerializeField]
        public TKey Key { get; private set; }

        /// <summary>
        /// 値
        /// </summary>
        [field: SerializeField]
        public TValue Value { get; private set; }
    }
}
