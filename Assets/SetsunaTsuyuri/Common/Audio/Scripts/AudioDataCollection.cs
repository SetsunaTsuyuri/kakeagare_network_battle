using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SetsunaTsuyuri
{
    /// <summary>
    /// オーディオデータ集
    /// </summary>
    [CreateAssetMenu(menuName = "SetsunaTsuyuri/Audio/DataCollection")]
    public class AudioDataCollection : DataCollection<AudioData>
    {
        /// <summary>
        /// データを取得する
        /// </summary>
        /// <param name="name">名前</param>
        /// <returns></returns>
        public AudioData GetDataOrDefault(string name)
        {
            return Data.FirstOrDefault(x => x.Name == name);
        }
    }
}
