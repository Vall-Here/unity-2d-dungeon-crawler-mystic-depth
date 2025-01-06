using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using DG.Tweening.Core.Easing;
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    public int maxHealth = 100;
    private int currentHealth;

    private EnemyAI enemyAI;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    public Slider healthBarSlider;
    private Animator myAnimator;
    public AudioClip dead;
    private AudioSource audioSource;
    private EnemyDropItem enemyDropItem;

    void Start()
    {
        currentHealth = maxHealth;
        enemyAI = GetComponent<EnemyAI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
        originalColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
        enemyDropItem = GetComponent<EnemyDropItem>();

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

        if (enemyAI != null)
        {
            enemyAI.TakeDamage(damage); 
        }

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



    private void FlashRed()
    {

        spriteRenderer.DOColor(Color.red, 0.1f)
            .OnComplete(() => spriteRenderer.DOColor(originalColor, 0.1f))
            .SetLoops(3, LoopType.Yoyo); 
    }

    private void Die()
    {
        audioSource.PlayOneShot(dead);
        Debug.Log("Enemy died.");
        myAnimator.SetBool("isDead", true);
    }

    public void DeadNotify()
    {   
        if(enemyDropItem != null)
        {
            enemyDropItem.DropItem();
        }
        Destroy(gameObject);
    }
}
