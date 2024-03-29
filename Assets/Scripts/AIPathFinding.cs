using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathFinding : MonoBehaviour
{
    [SerializeField]
    private Transform movePositionTransform;// the target destination to move to 
    [SerializeField]
    private Vector3 defaultLoc;

    private NavMeshAgent navMeshAgent;

    private Animator animator;
   
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        defaultLoc = transform.position;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.velocity == Vector3.zero)
        {
            animator.SetBool("isMoving", false);
        }
        if (movePositionTransform != null && navMeshAgent.destination != movePositionTransform.position)
        {
            animator.SetBool("isMoving", true);
            navMeshAgent.destination = movePositionTransform.position; 
        }
        else
        {
          navMeshAgent.destination = defaultLoc;
            
        }

    }

    public void SetTarget(Transform t)
    {
        movePositionTransform = t;
       
       
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            navMeshAgent.destination = this.transform.position; // have the agent stay in place and not push the player constantly
            //movePositionTransform.position = collision.transform.position;
            animator.SetBool("isMoving", false);
            navMeshAgent.velocity = Vector3.zero;
          
           
        }
    }

    
}
