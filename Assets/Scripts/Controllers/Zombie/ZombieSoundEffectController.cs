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
        // [SerializeField] private AudioClip _victoryAudioClip;
        [SerializeField] private AudioClip _hitAudioClip;
        [SerializeField] private AudioClip _defeatAudioClip;

        public AudioSource AudioSource => _audioSource;
        private AudioSource _audioSource;
        
        public void InitAudioSource()
        {
            // Asignar el componente AudioSource
            _audioSource = GetComponent<AudioSource>();
            // Asignamos el audio clip al AudioSource
            _audioSource.clip = AudioClip;
        }

        public void Play() => AudioSource.Play();
        public void Stop() => AudioSource.Stop();
        
        public void PlayOnHit() => AudioSource.PlayOneShot(_hitAudioClip);

        void Start()
        {
            InitAudioSource();
            // EventManager.instance.OnGameOver += OnGameOver;
        }
    }

}