using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrigger : MonoBehaviour
{
    public GameObject itemPrefab;
    public Vector3 bomSpawnOffset = new Vector3(0, 1, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 spawnPosition = transform.position + bomSpawnOffset;
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
            if(gameObject != null) {
                Destroy(gameObject);
            }
        }
    }
}
