using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : MonoBehaviour, IListenable
    {
        public AudioClip AudioClip => _audioClip;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private AudioClip _reloadClip;
        [SerializeField] private AudioClip _emptyClip;
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AudioClip;
        }

        public void PlayOnShot() => AudioSource.PlayOneShot(_shotClip);
        public void PlayOnReload() => AudioSource.PlayOneShot(_reloadClip);
        public void PlayOnEmpty() => AudioSource.PlayOneShot(_emptyClip);

        public void PlayOnLowLife()
        {
            AudioSource.loop = true;
            
            AudioSource.Play();
        }

        public void StopLowLifeSound()
        {
            AudioSource.loop = false;
            AudioSource.Stop();
        }
        public void Play() => AudioSource.Play();
        public void Stop() => AudioSource.Stop();
        void Start()
        {
            InitAudioSource();
        }
    }
}
