using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karaki
{
    public class EffectObjectController : MonoBehaviour
    {
        void EndOfAnimation()
        {
            Destroy(gameObject);
        }
    }
}