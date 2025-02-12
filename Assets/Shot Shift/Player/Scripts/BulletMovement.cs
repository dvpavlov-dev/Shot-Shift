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
}
