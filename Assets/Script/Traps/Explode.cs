using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour
{
    private Animator animator;
    public AudioClip sfxClip;
    private AudioSource audioSource;

    public float delayBomb = 2f;  // Waktu sebelum bom meledak
    public float explosionRadius = 2f; // Jarak ledakan bom
    public float damage = 3f;     // Jumlah damage yang diberikan kepada pemain

    private void Start()
    {
        // Setup audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sfxClip;
        audioSource.playOnAwake = false; 

        animator = GetComponent<Animator>();
        StartCoroutine(startIgnite());
        
      
       
    }


    private IEnumerator startIgnite()
    {
        
        yield return new WaitForSeconds(delayBomb);


        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitPlayers)
        {
            if (hit.CompareTag("Player"))  
            {

                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
        }


    }

    public void DestroyNotify(){
        Destroy(gameObject);
    }

    // private IEnumerator DestroyBombWithDelay(float delay)
    // {
 
    //     yield return new WaitForSeconds(delay);

    //     Destroy(gameObject);
    // }
}
