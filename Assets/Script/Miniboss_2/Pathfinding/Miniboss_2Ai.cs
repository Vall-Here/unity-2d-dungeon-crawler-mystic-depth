using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miniboss_2Ai : MonoBehaviour
{
private enum State
    {
        Patrol,
        Chase,
        Attack
    }

    [SerializeField] private State currentState = State.Patrol;

    [Header("========[Enemy AI]========")]
    public Transform player;

    [Header("========[Patrol Settings]========")]
    [Header("Buat Object kosong untuk setiap patrol point lalu assign ke array ini")]
    public Transform[] patrolPoints;

    [Header("========[Stat Settings]========")]
    public float speed = 2f;
    public float detectionRange = 10f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1.5f;
    public int damage = 10;

    public float waitTime = 2f;
    private bool isWaiting = false;
    private HashSet<GameObject> attackedEntities = new HashSet<GameObject>();

    private float lastAttackTime;
    private int currentPatrolIndex = 0;
    private GridManager gridManager;
    private List<Vector2> path;
    private int currentPathIndex = 0;

    private Animator animator;
    private PlayerHealth playerHealth;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        animator = GetComponent<Animator>();
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }

        for (int i = 0; i < patrolPoints.Length; i++)
        {
            Vector2 alignedPosition = gridManager.AlignPositionToGrid(patrolPoints[i].position);
            patrolPoints[i].position = alignedPosition;
        }
        attackedEntities.Clear();
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                if (distance <= detectionRange)
                {
                    currentState = State.Chase;
                }
                else
                {
                    Patrol();
                }
                break;

            case State.Chase:
                if (distance <= attackRange)
                {
                    currentState = State.Attack;
                }
                else if (distance > detectionRange)
                {
                    currentState = State.Patrol;
                }
                else
                {
                    MoveTowardsPlayer();
                }
                break;

            case State.Attack:
                if (distance > attackRange && distance <= detectionRange)
                {
                    currentState = State.Chase;
                }
                else if (distance > detectionRange)
                {
                    currentState = State.Patrol;
                }
                else
                {
                    Attack();
                }
                break;
        }
    }

    void MoveTowardsPlayer()
    {
        if (gridManager == null || player == null)
        {
            return;
        }

        Vector2 startPos = transform.position;
        Vector2 endPos = player.position;

        if (!gridManager.IsWalkable(endPos))
        {
            return;
        }

        path = gridManager.FindPath(startPos, endPos);
        if (path != null && path.Count > 0)
        {
            currentPathIndex = 0;
            StopAllCoroutines();
            StartCoroutine(FollowPath());
        }
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0 || isWaiting)
        {
            return;
        }

        Vector2 target = patrolPoints[currentPatrolIndex].position;
        path = gridManager.FindPath(transform.position, target);
        if (path != null && path.Count > 0)
        {
            currentPathIndex = 0;
            StopAllCoroutines();
            StartCoroutine(FollowPath());
        }
    }

    IEnumerator WaitAtPatrolPoint()
    {
        isWaiting = true;
        animator.SetFloat("speed", 0f);
        yield return new WaitForSeconds(waitTime);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        isWaiting = false;
        Patrol();
    }

    IEnumerator FollowPath()
    {
        while (currentPathIndex < path.Count)
        {
            Vector2 targetPosition = path[currentPathIndex];
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

        if (!isWaiting && patrolPoints.Length > 0 && Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            StartCoroutine(WaitAtPatrolPoint());
        }
    }

    void Attack()
    {
        if (Time.time > lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");

            attackedEntities.Clear();
        }
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
