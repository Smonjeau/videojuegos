using UnityEngine;

namespace Controllers
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectController : MonoBehaviour, IListenable
    {
        public AudioClip AudioClip => _audioClip;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _shotClip;
        [SerializeField] private AudioClip _reloadStartClip;
        [SerializeField] private AudioClip _reloadEndClip;
        [SerializeField] private AudioClip _emptyClip;
        // [SerializeField] private AudioClip _magazineReloadStartClip;
        // [SerializeField] private AudioClip _magazineReloadEndClip;
        // [SerializeField] private AudioClip _shellReloadStartClip;
        // [SerializeField] private AudioClip _shellReloadEndClip;
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AudioClip;
        }

        public void PlayOnShot() => AudioSource.PlayOneShot(_shotClip);
        // public void PlayOnMagazineReloadStart() => AudioSource.PlayOneShot(_magazineReloadStartClip);
        // public void PlayOnMagazineReloadEnd() => AudioSource.PlayOneShot(_magazineReloadEndClip);
        //
        // public void PlayOnShellReloadStart() => AudioSource.PlayOneShot(_shellReloadStartClip);
        // public void PlayOnShellReloadEnd() => AudioSource.PlayOneShot(_shellReloadEndClip);

        public void PlayOnReloadStart() => AudioSource.PlayOneShot(_reloadStartClip);
        public void PlayOnReloadEnd() => AudioSource.PlayOneShot(_reloadEndClip);
        public void PlayOnEmpty() => AudioSource.PlayOneShot(_emptyClip);

        public void Play() => AudioSource.Play();
        public void Stop() => AudioSource.Stop();
        void Start()
        {
            InitAudioSource();
        }
    }
}
