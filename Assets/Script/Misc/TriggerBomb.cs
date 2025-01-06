using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBomb : MonoBehaviour
{
    private PlayerHealth playerHealth;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Hit");
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        }
    }
}
