using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoldierSoundController : MonoBehaviour, IListenable
    {
        public AudioClip AudioClip => _audioClip;
        [SerializeField] private AudioClip _audioClip;
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;

        [SerializeField] private AudioClip hitByThrowableClip;
        
        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AudioClip;
        }
        
        void Start()
        {
            InitAudioSource();
        }

        public void Play()
        {
            AudioSource.Play();
        }

        public void Stop()
        {
            AudioSource.Stop();
        }

        public void PlayHitByThrowable()
        {
            AudioSource.PlayOneShot(hitByThrowableClip);
        }
    }
}