using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DelayedUIImage : MonoBehaviour
{
    public GameObject uiImage;         
    public float delayBeforeShow = 3f; 
    public float timeToShow = 5f;  
    public float fadeDuration = 1f; 
    public KeyCode keyToSkip ;

    private CanvasGroup canvasGroup; 

    private void Start()
    {
        
        canvasGroup = uiImage.GetComponent<CanvasGroup>();
        
   
        if (canvasGroup == null)
        {
            canvasGroup = uiImage.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false; 
        canvasGroup.blocksRaycasts = false; 

       
        StartCoroutine(ShowAndHideImage());
    }


    private void Update() {
        if (Input.GetKeyDown(keyToSkip)) {
            StopAllCoroutines();
            StartCoroutine(fadaway());
        }
    }
    

    private IEnumerator fadaway() {
        yield return new WaitForSeconds(1f);
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        yield break;
    }

    private IEnumerator ShowAndHideImage()
    {
  
        yield return new WaitForSeconds(delayBeforeShow);

 
        yield return FadeCanvasGroup(1f); 

    
        yield return new WaitForSeconds(timeToShow);

        // Fade-out Image
        yield return FadeCanvasGroup(0f);
    }

    private IEnumerator FadeCanvasGroup(float targetAlpha)
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timeElapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
        canvasGroup.interactable = targetAlpha > 0f;
        canvasGroup.blocksRaycasts = targetAlpha > 0f;
    }
}
