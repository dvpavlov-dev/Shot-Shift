using Shot_Shift.Actors.Weapon.Scripts;
using Shot_Shift.Configs.Sources;
using UnityEngine;

namespace Shot_Shift.Gameplay.Weapon.Scripts.Projectiles
{
    public class RocketController : ProjectileController<RocketConfigSource>
    {
        [SerializeField] private LayerMask _explosionLayer;
        
        protected override void CollideWithObject(Collider other)
        {
            if (!other.CompareTag("Player") && !other.CompareTag("Projectile"))
            {
                Explosion();
                Dispose();
            }
        }

        private void Explosion()
        {
            RaycastHit[] hits = new RaycastHit[100];
            int hitsCount = Physics.SphereCastNonAlloc(
                transform.position, 
                _projectileConfig.ExplosionRange, 
                transform.forward, 
                hits,
                _projectileConfig.ExplosionRange,
                _explosionLayer);
            
            if (hitsCount != 0)
            {
                for(int i = 0; i < hitsCount; i++)
                {
                    if (hits[i].transform.GetComponent<IDamageable>() is {} damageable)
                    {
                        if(hits[i].transform.CompareTag("Enemy"))
                        {
                            damageable.TakeDamage(_damage);
                        }

                        if (hits[i].transform.GetComponent<Rigidbody>() is {} rb)
                        {
                            rb.AddExplosionForce(_projectileConfig.ExplosionForce, transform.position, _projectileConfig.ExplosionRange);
                        }
                    }
                }
            }
        }
    }
}
