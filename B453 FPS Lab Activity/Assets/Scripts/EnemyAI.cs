using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;              // The speed at which the enemy moves.
    [SerializeField] private float stoppingDistance = 1f;   // The distance at which the enemy stops moving towards the player.

    [Header("Attack"),Min(5)]
    [SerializeField] private float attackRange = 5f;        // The range at which the enemy can attack the player.

    private NavMeshAgent agent;                             // Reference to the NavMeshAgent component.
    private Transform player;                               // Reference to the player's transform.

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= agent.stoppingDistance)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        agent.SetDestination(player.position);
    }

    private void Attack()
    {
        transform.LookAt(player);
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red,0.1f);
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player Hit");
            }
        }
    }
}
