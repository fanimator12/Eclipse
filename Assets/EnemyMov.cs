using UnityEngine;
using UnityEngine.AI;

public class EnemyMov : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatsGround, whatsPlayer;
    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float attackCooldown;
    bool attacked;
    public GameObject projectile;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Check for sight & attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) Chasing();
        if(playerInSightRange && playerInAttackRange) Attacking();
    }

    private void SearchWalkPoint()
    {
        // Random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if it's actually on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatsGround))
            walkPointSet = true;
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // When walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
    }

    private void Attacking()
    {
        // Check if enemy is standing
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!attacked)
        {
            // TODO attacks here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            attacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        attacked = false;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;

        if(health <= 0) Invoke(nameof(DestroyEnemy), .5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
