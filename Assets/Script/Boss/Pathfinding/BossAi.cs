using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    public float waitTime = 2f;

    private float lastAttackTime;
    private int currentPatrolIndex = 0;
    private bool isWaiting = false;
    private bool isTakingDamage = false; 
    private List<Vector2> path;
    private int currentPathIndex = 0;
    private GridManager gridManager;
    private Animator animator;
    private PlayerHealth playerHealth;
    private NodeBase rootNode;
    private HashSet<GameObject> attackedEntities = new HashSet<GameObject>();
    private Coroutine currentPathCoroutine;
    private bool isChasingPlayer = false;
    [SerializeField]private Image alertimage;

    
    [Header("Audio")]
    private AudioSource audioSource;
    public AudioClip walk;
    public AudioClip attack;
   
    
    private void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerHealth = player != null ? player.GetComponent<PlayerHealth>() : null;
    }
    void Start(){
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector2 alignedPosition = gridManager.AlignPositionToGrid(patrolPoints[i].position);
            patrolPoints[i].position = alignedPosition;
        }

        var patrolAction = new ActionNode(Patrol);
        var chaseAction = new ActionNode(ChasePlayer);
        var attackAction = new ActionNode(AttackPlayer);

        rootNode = new SelectorNode(new List<NodeBase> { attackAction, chaseAction, patrolAction });
    }

    void Update(){
        if (rootNode != null && !isTakingDamage) 
        {
            rootNode.Execute();
        }

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                ShowAlert();
            }
            else
            {
                HideAlert();
            }
        }
    }

    void ShowAlert()
    {
        Color newColor = alertimage.color;
        newColor.a = 1f; 
        alertimage.color = newColor;
    }

    void HideAlert()
    {
        Color newColor = alertimage.color;
        newColor.a = 0f;
        alertimage.color = newColor;
    }

    bool Patrol()
    {
        if (patrolPoints.Length == 0 || isWaiting || isTakingDamage)
            return false;

        if (isChasingPlayer)
        {
            isChasingPlayer = false;
        }

        Vector2 target = patrolPoints[currentPatrolIndex].position;
        float distanceToPatrolPoint = Vector2.Distance(transform.position, target);

        if (distanceToPatrolPoint < 0.1f)
        {
            if (!isWaiting)
            {
                StartCoroutine(WaitAtPatrolPoint());
            }
            return true;
        }

        path = gridManager.FindPath(transform.position, target);
        if (path != null && path.Count > 0)
        {
            currentPathIndex = 0;
            StartPathCoroutine();
            return true;
        }
        return false;
    }

    bool ChasePlayer()
    {
        if (player == null || isTakingDamage)
            return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange || distanceToPlayer <= attackRange)
            return false;

        isChasingPlayer = true;
        path = gridManager.FindPath(transform.position, player.position);
        if (path != null && path.Count > 0)
        {

            currentPathIndex = 0;
            StartPathCoroutine();
            return true;
        }
        return false;
    }

    bool AttackPlayer()
    {
        if (player == null || isTakingDamage)
            return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > attackRange)
            return false;

        if (Time.time > lastAttackTime + attackCooldown)
        {
            audioSource.PlayOneShot(attack);
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
            attackedEntities.Clear();
        }
        return true;
    }

    public void AttackNotify()
    {
        if (playerHealth != null)
        {
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= attackRange)
            {
                if (!attackedEntities.Contains(player.gameObject))
                {
                    playerHealth.TakeDamage(damage);
                    attackedEntities.Add(player.gameObject);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        isTakingDamage = true; 
        StartCoroutine(RecoverFromDamage());
    }

    IEnumerator RecoverFromDamage()
    {
        yield return new WaitForSeconds(0.5f);
        isTakingDamage = false; 
    }

    void StartPathCoroutine()
    {
        if (currentPathCoroutine != null)
        {
            StopCoroutine(currentPathCoroutine);
        }
        currentPathCoroutine = StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        while (currentPathIndex < path.Count)
        {
            Vector2 targetPosition = path[currentPathIndex];
            if (!gridManager.IsWalkable(targetPosition))
                yield break;

            while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                

                animator.SetFloat("MoveX", direction.x);
                animator.SetFloat("MoveY", direction.y);
                animator.SetFloat("speed", speed);

                yield return null;
            }
            currentPathIndex++;
        }
        animator.SetFloat("speed", 0f);
    }

    IEnumerator WaitAtPatrolPoint()
    {
        isWaiting = true;
        animator.SetFloat("speed", 0f);
        yield return new WaitForSeconds(waitTime);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        isWaiting = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        if (path != null)
        {
            Gizmos.color = Color.green;
            for (int i = 0; i < path.Count - 1; i++)
            {
                Gizmos.DrawLine(path[i], path[i + 1]);
            }
        }
    }
}
