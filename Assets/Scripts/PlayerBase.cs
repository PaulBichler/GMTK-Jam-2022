using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().ReceiveDamage(new DamageInfo(1000));
            PlayerManager.Instance.BaseCollision();
        }
    }
}
