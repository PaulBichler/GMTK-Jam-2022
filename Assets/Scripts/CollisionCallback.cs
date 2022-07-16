using System;
using UnityEngine;
using UnityEngine.Events;

public class CollisionCallback : MonoBehaviour
{
    [SerializeField] private string checkTag;
    [SerializeField] private bool destroyOther;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onCollisionEnter;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (checkTag.Length == 0 || col.gameObject.CompareTag(checkTag))
        {
            onTriggerEnter.Invoke();
            
            if(destroyOther)
                Destroy(col.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (checkTag.Length == 0 || col.gameObject.CompareTag(checkTag))
        {
            onCollisionEnter.Invoke();
            
            if(destroyOther)
                Destroy(col.gameObject);
        }
    }
}
