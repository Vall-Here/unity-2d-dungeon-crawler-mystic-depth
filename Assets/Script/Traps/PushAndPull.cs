using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPull : MonoBehaviour
{
    public float pushPullSpeed = 2f;
    private GameObject objectToPushPull;
    private bool isPushing = false;

    private Rigidbody2D playerRb;

    private PlayerAudioController playerAudioController;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }

    void Update()
    {
        HandleInput();

        if (objectToPushPull != null)
        {
            if (isPushing)
            {
                SetObjectToDynamic();
                MoveObject();
            }
            else
            {
                SetObjectToStatic();
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (objectToPushPull != null)
            {
                isPushing = true;
                playerAudioController.PlayPushing();
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            isPushing = false;
        }
    }

    private void MoveObject()
    {
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        movementInput = movementInput.normalized;
        objectToPushPull.GetComponent<Rigidbody2D>().velocity = movementInput * pushPullSpeed;
    }

    private void SetObjectToDynamic()
    {
        Rigidbody2D rb = objectToPushPull.GetComponent<Rigidbody2D>();
        if (rb.bodyType != RigidbodyType2D.Dynamic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void SetObjectToStatic()
    {
        Rigidbody2D rb = objectToPushPull.GetComponent<Rigidbody2D>();
        if (rb.bodyType != RigidbodyType2D.Static)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            objectToPushPull = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Pushable"))
        {
            if (objectToPushPull == collision.gameObject)
            {
                SetObjectToStatic();
                objectToPushPull = null;
            }
        }
    }
}
