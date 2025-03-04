using System;
using UnityEngine;
using Zenject;

namespace Shot_Shift.Infrastructure.Scripts.Services
{
    public class SettingsService : IInitializable
    {
        public bool SoundStatus
        {
            get => _soundStatus;
            set
            {
                _soundStatus = value;
                OnSoundStatusChanged?.Invoke();
                SaveSettings();
            }
        }

        public bool InvertControlStatus
        {
            get => _invertControlStatus;
            set
            {
                _invertControlStatus = value;
                OnInvertControlStatusChanged?.Invoke();
                SaveSettings();
            }
        }

        public Action OnSoundStatusChanged;
        public Action OnInvertControlStatusChanged;
        
        private bool _soundStatus;
        private bool _invertControlStatus;
        
        public void Initialize()
        {
            LoadSettings();
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt("SOUND", SoundStatus ? 1 : 0);
            PlayerPrefs.SetInt("INVERT_CONTROL", InvertControlStatus ? 1 : 0);
            
            PlayerPrefs.Save();
        }

        private void LoadSettings()
        {
            SoundStatus = PlayerPrefs.GetInt("SOUND") == 1;
            InvertControlStatus = PlayerPrefs.GetInt("INVERT_CONTROL") == 1;
        }
    }
}
