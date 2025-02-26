using Shot_Shift.Actors.Weapon.Scripts;
using UnityEngine;

namespace Shot_Shift.Gameplay.Drop.Scripts
{
    public class MedkitDropComponent : DropComponent
    {
        public override void TakeDrop(GameObject actor)
        {
            if (actor.CompareTag("Player") && actor.GetComponent<IDamageable>() is {} damageable)
            {
                damageable.TakeHealing(10);
                Destroy(gameObject);
            }
        }
    }
}
