using UnityEngine;

namespace CodeBase.Infrastructure.Services.Audio
{
    public interface IAudioService
    {
        void Init();
        void Play(AudioClip clip);
    }
}
