using UnityEngine;

public class BulletDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Projectile"))
            Destroy(col.gameObject);
    }
}
