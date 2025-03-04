using Shot_Shift.Infrastructure.Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Shot_Shift.UI.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundSoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _backgroundSound;
        
        private AudioSource _audioSource;
        private SettingsService _settingsService;

        [Inject]
        private void Constructor(SettingsService settingsService) 
        {
            _settingsService = settingsService;
        }
        
        void Start()
        {
            SetupAudioSource();

            _settingsService.OnSoundStatusChanged += SyncSound;
            
            SyncSound();
        }
        
        private void SetupAudioSource()
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.clip = _backgroundSound;
            _audioSource.playOnAwake = false;
            _audioSource.loop = true;
        }

        private void SyncSound()
        {
            if (_settingsService.SoundStatus)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }

        private void OnDestroy()
        {
            if (SceneManager.sceneCount == 0) return;
            
            _settingsService.OnSoundStatusChanged -= SyncSound;
        }
    }
}
