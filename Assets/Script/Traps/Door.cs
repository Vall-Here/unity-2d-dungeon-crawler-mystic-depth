using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InventorySystem;
using System;

public class Door : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D triggerCollider; 
    [SerializeField] private BoxCollider2D doorCollider;

    [SerializeField] String keyName;    
    public Sprite openDoor; 

    public AudioClip sfxClip;
    public AudioClip lockedSfxClip;
    private AudioSource audioSource;

    private void Start() {
        audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false; 
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (triggerCollider == null || doorCollider == null) {
            BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
            triggerCollider = colliders[0];
            doorCollider = colliders[1];
        }
    }

    private void Update() {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    private void OpenDoor() {
        // LEtakkan disini logic kuncinya
        if(InventoryController.instance.CountItems("Hotbar", keyName) > 0)
        {
            InventoryController.instance.RemoveItem("Hotbar", keyName, 1);
            Vector2 originalSize = spriteRenderer.sprite.bounds.size;

            if(spriteRenderer != null) {
                spriteRenderer.sprite = openDoor;
                Vector2 newSize = spriteRenderer.sprite.bounds.size;
                Vector3 scale = transform.localScale;
                scale.x *= originalSize.x / newSize.x;
                scale.y *= originalSize.y / newSize.y;
                transform.localScale = scale;
            }
            audioSource.clip = sfxClip;
            if(doorCollider != null) {
                audioSource.Play();
                Destroy(doorCollider);
            }
        }else   
        {
            // audioSource.clip = lockedSfxClip;
            audioSource.PlayOneShot(lockedSfxClip);
            Debug.Log("You need a key to open this door");
        }
    }
}
