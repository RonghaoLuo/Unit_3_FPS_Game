using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent myAgent;
    //[SerializeField] private Transform target;
    [SerializeField] private Vector3 destination;

    private NpcState currentState;

    void Start()
    {
        //InvokeRepeating("FollowTarget", 1, 5);
        ChangeState(new WanderNpcState(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnStateRun();
        }
    }

    public void ChangeState(NpcState newState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = newState;
        currentState.OnStateEnter();
    }

    //private void FollowTarget()
    //{
    //    myAgent.SetDestination(target.position);
    //}

    public void SetAgentDestination(Vector3 destination)
    {
        myAgent.SetDestination(destination);
    }

    public bool IsMoving()
    {
        return myAgent.remainingDistance > myAgent.stoppingDistance;// || !myAgent.isStopped;
                                                                        // && myAgent.velocity.magnitude > 5f;
    }
}
