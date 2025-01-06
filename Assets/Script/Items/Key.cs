using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;


public class Key : MonoBehaviour
{
    public enum KeyType
    {
        MasterKey,
        NormalKey,
        ChestKey
    }

    [SerializeField] public string keyName;
    [SerializeField] private KeyType keyType;
    [SerializeField] private AnimationCurve animCurve;
    private float heightY = 1.5f;
    private float popDuration = 1.5f;
    [SerializeField] private AudioClip pickupSound;

    private AudioSource audioSource;


    private void Start() {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(animCurvesSpawnRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            audioSource.PlayOneShot(pickupSound);
            if (!InventoryController.instance.InventoryFull("Hotbar", keyName)) {
                InventoryController.instance.AddItem("Hotbar", keyName);
                StartCoroutine(DestroyAfterSound(pickupSound.length));
            } else {
                Debug.Log("Inventory Cannot Fit Item");
            }
        }
    }

    private IEnumerator DestroyAfterSound(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator animCurvesSpawnRoutine() {
        Vector2 starPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-0.5f, 0.5f);
        float randomY = transform.position.y + Random.Range(-0.2f, 0.2f);

        Vector2 endPoint = new Vector2(randomX, randomY);
        float timePassed = 0f;

        while (timePassed < popDuration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(starPoint, endPoint, linearT) + new Vector2(0f, height);

            yield return null;
        }
    }

}
