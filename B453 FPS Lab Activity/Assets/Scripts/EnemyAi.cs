using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float stoppingDistance = 5f;

    [SerializeField, Min(5)] float attackRange = 5f;

    private NavMeshAgent agent;
    private Transform player;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;

        agent.speed = speed;
        agent.stoppingDistance = stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= agent.stoppingDistance)
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
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.magenta, 0.1f);

        if(Physics.Raycast(transform.position,transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Haha");
            }
        }
    }
}
