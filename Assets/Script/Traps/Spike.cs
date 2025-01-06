using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikeTilemap : MonoBehaviour
{
    public bool isVisible = true;
    public float cooldown = 2f;
    private TilemapRenderer tilemapRenderer;
    private TilemapCollider2D tilemapCollider;
    public AudioClip sfxClip;
    private AudioSource audioSource;
    private PlayerHealth playerHealth;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = sfxClip;
        audioSource.playOnAwake = false; 
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider = GetComponent<TilemapCollider2D>();
        StartCoroutine(ToggleVisibility());
    }

    private IEnumerator ToggleVisibility()
    {
        while (true)
        {
            isVisible = !isVisible; 
            UpdateVisibility();
            audioSource.Play();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private void UpdateVisibility()
    {
        tilemapRenderer.enabled = isVisible;
        tilemapCollider.enabled = isVisible;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isVisible && collision.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null) {
                    playerHealth.TakeDamage(1f);
                }
        }
    }
}