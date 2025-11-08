using UnityEngine;
using UnityEngine.AI;

public class StateNpc : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent myAgent;
    //[SerializeField] private Transform target;
    [SerializeField] protected Vector3 destination;

    protected NpcState currentState;

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

    public void SetAgentDestination()
    {
        myAgent.SetDestination(destination);
    }

    public void SetAgentDestination(Vector3 destination)
    {
        this.destination = destination;
        SetAgentDestination();
    }

    public bool IsMoving()
    {
        return myAgent.remainingDistance > myAgent.stoppingDistance;// || !myAgent.isStopped;
                                                                    // && myAgent.velocity.magnitude > 5f;
    }

}
