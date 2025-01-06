using UnityEngine;
using System.Collections.Generic;

public class DamageSource : MonoBehaviour
{
    public int damageAmount = 1;
    private HashSet<GameObject> damagedEntities = new HashSet<GameObject>();

    private void OnEnable()
    {
        damagedEntities.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject entity = collision.gameObject;

        if (!damagedEntities.Contains(entity))
        {
            IDamageable damageable = collision.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                damagedEntities.Add(entity);
            }
        }
    }
}
