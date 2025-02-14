using UnityEngine;

namespace Shot_Shift.Configs.Sources
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfigSource : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public float Health;
    }
}
