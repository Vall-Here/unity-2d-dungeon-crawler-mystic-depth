using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelLoader : MonoBehaviour
{

    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     LoadNextLevel();
        // }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel("Level1"));
    }

    IEnumerator LoadLevel(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(name);

        // AsyncOperation operation = SceneManager.Instance.LoadLevel(name);
        // while (!operation.isDone)
        // {
        //     yield return null;
        // }
    }
}
