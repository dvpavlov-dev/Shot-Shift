using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "BulletTimeConfig", menuName = "Configs/Abilities/BulletTime")]
    public class BulletTimeConfigSource : ScriptableObject
    {
        [SerializeField] private float _slowdownInPercent;
        [SerializeField] private float _durationSlowdownInSeconds;
        [SerializeField] private int _cost;
        
        public float SlowdownInPercent => _slowdownInPercent;
        public float DurationSlowdownInSeconds => _durationSlowdownInSeconds;
        public int Cost => _cost;
    }
}
