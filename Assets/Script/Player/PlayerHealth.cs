using UnityEngine;
using UnityEngine.UI; // Untuk UI Slider
using UnityEngine.SceneManagement; 

public class PlayerHealth : MonoBehaviour
{
    private UIControl uIControl;
    [Header("Health Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Impulse")]
    [SerializeField] private ScreenShakeManager screenShakeManager;

    [Header("UI Elements")]
    public Slider healthSlider; 

    [Header("Death Settings")]
    public bool isDead = false;


    [Header("For Developing Purpose")]
    public bool isNgecit = false;

    private Animator animator;

    private PlayerAudioController playerAudioController;

    private void Awake() {
        playerAudioController = GetComponent<PlayerAudioController>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        uIControl = FindObjectOfType<UIControl>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    private void Update() {
        if (isDead && !animator.GetBool("isDead"))
        {
            Die();
        }
    }


    public void TakeDamage(float damageAmount)
    {
        if (isDead)
            return;

        screenShakeManager.ShakeScreen();
        if (!isNgecit) {
            currentHealth -= damageAmount;
            UpdateHealthUI();

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }else {
            Debug.Log("I AM GOD HUAHAHAHAHAHAH");
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
    }
    void Die()
    {
        isDead = true;
        playerAudioController.PlayDead();
        animator = GetComponent<Animator>();
        Debug.Log("Player has died.");
        animator.SetBool("isDead", true);
        GetComponent<PlayerController>().enabled = false;
    }

    public void Dead() {
        Destroy(gameObject, 2f);
        uIControl.OpenGameOver();
    }

    public void Heal(float healAmount)
    {
        if (isDead)
            return;
        Debug.Log("Healing " + healAmount + " health points.");

        currentHealth += healAmount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }
}
