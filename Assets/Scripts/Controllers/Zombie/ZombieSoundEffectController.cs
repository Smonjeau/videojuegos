using UnityEngine;

// Agregamos un componente obligatorio -> Esto fueza a que unity agregue 
// el componente si no existe en el objeto.
namespace Controllers
{
    
    [RequireComponent(typeof(AudioSource))]
    public class ZombieSoundEffectController : MonoBehaviour, IListenable
    {
        public AudioClip AudioClip => _audioClip;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _hitAudioClip;
        [SerializeField] private AudioClip _throwAudoClip;
        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        public void InitAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = AudioClip;
        }

        public void Play() => AudioSource.Play();
        public void Stop() => AudioSource.Stop();
        
        public void PlayOnHit() => AudioSource.PlayOneShot(_hitAudioClip);
        public void PlayOnZombieThrow() => AudioSource.PlayOneShot(_throwAudoClip);


        void Start() =>  InitAudioSource();
    }

}