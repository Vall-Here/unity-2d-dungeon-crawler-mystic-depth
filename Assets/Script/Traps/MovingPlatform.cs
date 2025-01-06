using System.Collections;
using UnityEngine;

public class VerticalMovingPlatform : MonoBehaviour
{
    public float speed = 2f;            
    public float startY;          
    public float endY;            
    private bool movingDown = true;
    private bool isPlayerOnPlatform = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
        }
    }

    public bool IsPlayerOnPlatform()
    {
        return isPlayerOnPlatform;
    }

    void Update()
    {
        // Debug.Log(startY);
        if (movingDown)
        {   
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, endY, transform.position.z), speed * Time.deltaTime);
            if (transform.position.y <= endY)
            {
                movingDown = !movingDown;                   
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startY, transform.position.z), speed * Time.deltaTime);
            if (transform.position.y >= startY)
            {
                movingDown = !movingDown;
                Debug.Log(transform.position.y);
            }
        }
    }

}