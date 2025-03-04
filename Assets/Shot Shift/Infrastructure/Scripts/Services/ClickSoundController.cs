using UnityEngine;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    [RequireComponent(typeof(AudioSource))]
    public class ClickSoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickSound;

        private AudioSource _audioSource;
        
        public void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _clickSound;
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;
        }

        public void PlayClickSound()
        {
            _audioSource.Play();
        }
    }
}
