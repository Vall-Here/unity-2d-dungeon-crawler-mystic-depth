using UnityEngine;

public class FallDetection2D : MonoBehaviour
{
    public VerticalMovingPlatform platformStatus;
    private bool isPlayerInJungle = false;
    private bool fallDown = false; 
    private PlayerHealth playerHealth;

    void Update()
    {
        if (isPlayerInJungle && !platformStatus.IsPlayerOnPlatform() && !fallDown)
        {
            Debug.Log("Jatuh");
            fallDown = true;
            playerHealth.TakeDamage(10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInJungle = true;
            fallDown = false;
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInJungle = false;
        }
    }
}
