using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InventorySystem;


public class HealthParticle : MonoBehaviour
{
    private int healthAmount = 4;
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
            collision.GetComponent<PlayerHealth>().Heal(healthAmount);
            StartCoroutine(DestroyAfterSound(pickupSound.length));
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
