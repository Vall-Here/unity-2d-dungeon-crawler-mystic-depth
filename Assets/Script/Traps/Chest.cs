using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Sprite closedChestSprite;
    public Sprite openChestSprite; 
    public GameObject itemPrefab;
    public Transform itemSpawnPoint;
    private bool isOpen = false;
    private bool isPlayerNearby = false;

    private SpriteRenderer spriteRenderer;
    public AudioClip sfxClip;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sfxClip;
        audioSource.playOnAwake = false; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closedChestSprite;
    }

    private void Update()
    {
        // Ubah apa enaknya gue juga gtw enaknya apa
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F) && !isOpen)
        {
            OpenChest();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void OpenChest()
    {
        isOpen = true;
        spriteRenderer.sprite = openChestSprite; 
        audioSource.Play();

        if (itemPrefab != null && itemSpawnPoint != null)
        {
            Instantiate(itemPrefab, itemSpawnPoint.position, Quaternion.identity);
        }
    }
}
