using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
    public class AudioService : IAudioService
    {
        private AudioSource _audioSource;
        public void Init()
        {
            var audioParent = new GameObject(nameof(AudioService));
            _audioSource = audioParent.AddComponent<AudioSource>();
            Object.DontDestroyOnLoad(audioParent);
        }

        public void Play(AudioClip clip) =>
            _audioSource.PlayOneShot(clip);
    }
}
