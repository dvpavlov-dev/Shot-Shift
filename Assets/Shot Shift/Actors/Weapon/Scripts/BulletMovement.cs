using Shot_Shift.Actors.Weapon.Scripts;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(transform.right * Time.deltaTime * 20f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() is {} damageable && !other.CompareTag("Player"))
        {
            damageable.TakeDamage(10);
            Destroy(gameObject);
        }
    }
}
