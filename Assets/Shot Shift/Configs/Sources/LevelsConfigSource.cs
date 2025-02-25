using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class LevelsConfigSource : ScriptableObject
    {
        [SerializeField] private List<Level> _levels = new();
        
        public List<Level> Levels => _levels;
        
        [Serializable]
        public struct Level
        {
            [SerializeField] private string _nameLevel;
            [SerializeField] private int _enemyCount;
            [SerializeField] private bool _isTimerNeeded;
            [SerializeField] private int _timerIntervalInSeconds;

            public int EnemyCount => _enemyCount;
            public bool IsTimerNeeded => _isTimerNeeded;
            public int TimerIntervalInSeconds => _timerIntervalInSeconds;
        }
    }
}
