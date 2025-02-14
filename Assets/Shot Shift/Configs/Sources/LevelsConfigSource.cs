using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "LevelsConfig", menuName = "Configs/LevelsConfig")]
    public class LevelsConfigSource : ScriptableObject
    {
        public List<Level> levels = new List<Level>();
        
        [Serializable]
        public struct Level
        {
            public string NameLevel;
            public int EnemyCount;
            public bool IsTimerNeeded;
            public int TimerIntervalInSeconds;
        }
    }
}
