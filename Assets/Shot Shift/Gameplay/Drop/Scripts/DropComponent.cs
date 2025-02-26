using UnityEngine;

namespace Shot_Shift.Gameplay.Drop.Scripts
{
    public abstract class DropComponent : MonoBehaviour
    {
        public abstract void TakeDrop(GameObject actor);

        private void OnTriggerEnter(Collider other)
        {
            TakeDrop(other.gameObject);
        }
    }
}
