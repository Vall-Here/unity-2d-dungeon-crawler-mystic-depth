using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        walk,
        attack,
        pushing,
        idle,
        die,
    }

    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private bool isPointerOverUI = false;

    [Header("========[Player Controller]========")]
    [ReadOnly] [SerializeField] private PlayerState e_currentPlayerState = PlayerState.idle; 

    [Header("========[Speed Settings]========")]
    [Header("Di BAN HANAFI.***")]
    [ReadOnly] [SerializeField] private float speed = 3f;
    private PlayerControls playerControls;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection;
    private bool facingLeft = false;
    private bool e_stateLocked = false;
    private bool e_canMove = true;
    private Rigidbody2D rb;

    [Header("========[Attack Collider Settings]========")]
    [SerializeField] public GameObject attackColliderUp;
    [SerializeField] public GameObject attackColliderDown;
    [SerializeField] public GameObject attackColliderLeft;
    [SerializeField] public GameObject attackColliderRight;


    [Header("========[Player Pull & Push Config]========")]
    [SerializeField] private Transform pushPullDetectionPoint; 
    [SerializeField] private float pushPullDetectionRadius = 1f; 
    [SerializeField] private LayerMask pushPullLayer; 
    private GameObject currentPushPullObject;

    private PlayerAudioController playerAudioController;


    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        playerAudioController = GetComponent<PlayerAudioController>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Attack.Perform.performed += _ => PerformAttack();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        playerControls.Attack.Perform.performed -= _ => PerformAttack();
    }

    private void Update()
    {
        isPointerOverUI = EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        moveInput = playerControls.Move.Movement.ReadValue<Vector2>().normalized;


        myAnimator.SetBool("isWalking", moveInput != Vector2.zero);

        if (playerControls.Action.Pnp.IsPressed())
        {
            e_currentPlayerState = PlayerState.pushing;
            myAnimator.SetBool("isPnp", true);
        }
        else
        {
            if (e_currentPlayerState == PlayerState.pushing)
            {
                e_currentPlayerState = PlayerState.idle;
            }
            myAnimator.SetBool("isPnp", false);
        }

        if (e_canMove && moveInput != Vector2.zero)
        {
            lastMoveDirection = moveInput;
            e_currentPlayerState = PlayerState.walk;

            myAnimator.SetFloat("MoveX", moveInput.x);
            myAnimator.SetFloat("MoveY", moveInput.y);

            if (moveInput.x < 0)
            {
                mySpriteRenderer.flipX = true;
                FacingLeft = true;
            }
            else if (moveInput.x > 0)
            {
                mySpriteRenderer.flipX = false;
                FacingLeft = false;
            }
        }
        else if (e_currentPlayerState != PlayerState.attack && e_currentPlayerState != PlayerState.pushing)
        {
            e_currentPlayerState = PlayerState.idle;
        }
    }



    private void FixedUpdate()
    {
            Move();
    }


    private void Move()
    {
        rb.MovePosition(rb.position + moveInput * (speed * Time.fixedDeltaTime));
        if (moveInput.magnitude > 0.1f && !playerAudioController.isPlaying())
        {
            playerAudioController.PlayWalk();
        }
    }

    private void PerformAttack()
    {
        if (isPointerOverUI)
        {
            return;
        }

        if (e_currentPlayerState != PlayerState.attack && !e_stateLocked)
        {
            e_currentPlayerState = PlayerState.attack;
            myAnimator.SetTrigger("Attack");
            e_stateLocked = true;
            e_canMove = false;
            EnableAttackCollider();
            playerAudioController.PlayAttack();
        }
    }

    public void OnAttackFinish()
    {
        DisableAllColliders();
        e_stateLocked = false;
        e_currentPlayerState = PlayerState.idle;
        e_canMove = true;
    }

    private void EnableAttackCollider()
    {
        DisableAllColliders();

        Vector2 attackDirection = moveInput != Vector2.zero ? moveInput : lastMoveDirection;

        if (attackDirection.y > 0)
        {
            attackColliderUp.SetActive(true);
        }
        else if (attackDirection.y < 0)
        {
            attackColliderDown.SetActive(true);
        }
        else if (attackDirection.x < 0)
        {
            attackColliderLeft.SetActive(true);
        }
        else if (attackDirection.x > 0)
        {
            attackColliderRight.SetActive(true);
        }
    }

    private void DisableAllColliders()
    {
        attackColliderUp.SetActive(false);
        attackColliderDown.SetActive(false);
        attackColliderLeft.SetActive(false);
        attackColliderRight.SetActive(false);
    }

    public void StopAttackCollider()
    {
        DisableAllColliders();
    }

    private void OnDrawGizmosSelected()
    {
        if (pushPullDetectionPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pushPullDetectionPoint.position, pushPullDetectionRadius);
        }
    }

}
