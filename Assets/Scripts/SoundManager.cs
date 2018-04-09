using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LIL
{
    public class SoundManager
    {

        public IEnumerator AudioFade(AudioSource source, float fadingTime)
        {
            float startVolume = source.volume;
            while (source.volume > 0)
            {
                source.volume -= startVolume * Time.deltaTime / fadingTime;
                yield return null;
            }

            source.Stop();
            source.volume = startVolume;
        }
    }
}

