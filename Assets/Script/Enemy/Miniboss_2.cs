using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Miniboss_2 : MonoBehaviour, IDamageable
{
[SerializeField]
    public int maxHealth = 100;
    private int currentHealth;

    private Miniboss_2Ai Miniboss_2Ai;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Slider healthBarSlider;
    private Animator myAnimator;

    void Start()
    {
        Debug.Log(maxHealth);
        currentHealth = maxHealth;
        Miniboss_2Ai = GetComponent<Miniboss_2Ai>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        originalColor = spriteRenderer.color;

        if (healthBarSlider == null)
        {
            healthBarSlider = GetComponentInChildren<Slider>();
        }

        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
            return;

        currentHealth -= damage;
        UpdateHealthBar();

        

        // StartCoroutine(FlashRed());
        FlashRed();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthBarSlider.value = currentHealth;
    }

    // private IEnumerator FlashRed()
    // {
    //     int flashCount = 3;
    //     float flashDuration = 0.1f;

    //     for (int i = 0; i < flashCount; i++)
    //     {
    //         spriteRenderer.color = Color.red;
    //         yield return new WaitForSeconds(flashDuration);
    //         spriteRenderer.color = originalColor;
    //         yield return new WaitForSeconds(flashDuration);
    //     }
    // }

    private void FlashRed()
    {

        spriteRenderer.DOColor(Color.red, 0.1f)
            .OnComplete(() => spriteRenderer.DOColor(originalColor, 0.1f))
            .SetLoops(3, LoopType.Yoyo); 
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        myAnimator.SetBool("isDead", true);
    }

    public void DeadNotify()
    {
        Destroy(gameObject);
    }
}
