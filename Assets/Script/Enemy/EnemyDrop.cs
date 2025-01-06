using UnityEngine;
using System.Collections;

public class EnemyDropItem : MonoBehaviour
{
    public GameObject itemPrefab;
    
    public float dropDelay = 0.5f;

    public void DropItem()
    {
        StartCoroutine(IEDropItem());
    }

    public IEnumerator IEDropItem()
    {
        yield return new WaitForSeconds(dropDelay);

        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
