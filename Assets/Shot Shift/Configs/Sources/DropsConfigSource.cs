using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "DropsConfig", menuName = "Configs/DropsConfig")]
    public class DropsConfigSource : ScriptableObject
    {
        [SerializeField] private GameObject _medkitDropPrefab;
        [SerializeField] private GameObject _coinsDropPrefab;
        
        public GameObject MedkitDropPrefab => _medkitDropPrefab;
        public GameObject CoinsDropPrefab => _coinsDropPrefab;
    }
}
