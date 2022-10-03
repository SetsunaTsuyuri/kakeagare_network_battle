using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// ゲームを開始する前のカウントダウンスクリプト
/// </summary>

namespace Tsuguhiko
{
    
    public class StartCountDown : MonoBehaviour
    {
        [SerializeField, Header("カウントテキスト")] TextMeshProUGUI _countText;

       // [SerializeField, Header("効果音のクリップ")] AudioClip[] _audioClips;

      // [SerializeField,Header("効果音のソース")] AudioSource _audioSource;

        void Start()
        {
            //  _audioSource = GetComponent<AudioSource>();

            StartCoroutine(CountDown());
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IEnumerator CountDown()
        {
            _countText.text = "3";

            yield return new WaitForSeconds(1);

            _countText.text = "2";

            yield return new WaitForSeconds(1);

            _countText.text = "1";

            yield return new WaitForSeconds(1);

            _countText.text = "START!!";
        }
    }
}