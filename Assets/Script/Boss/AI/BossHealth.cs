using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 150;
    [SerializeField] private Material redFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;
    

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;

    private Animator myAnimator;


    private int currentHealth;

    private void Awake(){
        myAnimator = GetComponent<Animator>();
    }

    public void Start(){
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;

    }

    public void TakeDamage(int damage){
        currentHealth -= damage;

        Debug.Log(currentHealth);
        DetectDeath();
        StartCoroutine(FlashRoutine());
    }

    public void DetectDeath(){
        if (currentHealth <= 0) {
            myAnimator.SetBool("isDead", true);
        }
    }

    public IEnumerator FlashRoutine() {
        spriteRenderer.material = redFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        DetectDeath();
    }
}
