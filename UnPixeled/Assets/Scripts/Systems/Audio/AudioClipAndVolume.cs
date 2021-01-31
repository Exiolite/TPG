using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.Audio
{
    [System.Serializable]
    public class AudioClipAndVolume
    {
        [SerializeField] private AudioClip audioClip;
        [Range(0.0f, 1.0f), SerializeField] private float volume = 0;

        public void PlaySound(ref AudioSource audioSource)
        {
            if (audioClip == null) return;
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioClip);
        }
    }

    [System.Serializable]
    public class AudioClipData
    {
        [SerializeField] private AudioClipAndVolume[] array;
        
        public AudioClipAndVolume GetRandomAudio()
        {
            if (array.Length != 0) return array[Random.Range(0, array.Length)];
            throw new System.Exception("AudioClip array have no elements");
        }
    }
}