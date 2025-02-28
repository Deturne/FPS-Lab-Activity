using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAi : MonoBehaviour,IDamagable
{
    [SerializeField] float speed = 5f;
    [SerializeField] float stoppingDistance = 5f;
    [SerializeField] GameObject[] waypoints;

    [SerializeField, Min(5)] float attackRange = 5f;

    private NavMeshAgent agent;
    private Transform player;
    //Raycast hit;

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
        RaycastHit hit;

       
        
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.blue, .5f);
       if(Physics.Raycast(transform.position,transform.forward, out hit, attackRange)){

            if(hit.collider.CompareTag("Player")){
            agent.SetDestination(player.position);
            }
            else{
                
                agent.SetDestination(waypoints[0].transform.position);
                StartCoroutine(wait());
            }
       }
    }

    private void Attack()
    {
        transform.LookAt(player);

        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.magenta, 6f);

        if(Physics.Raycast(transform.position,transform.forward, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Haha");
            }
        }
    }

    protected IEnumerator wait(){
        
        yield return new WaitForSeconds(6f);
        agent.SetDestination(waypoints[1].transform.position);

    }

    public void TakeDamage(int damage){
    
        agent.SetDestination(player.position);
        
    }
}
