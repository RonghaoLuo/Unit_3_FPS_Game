using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 destination;

    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("FollowTarget", 1, 5);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void FollowTarget()
    {
        myAgent.SetDestination(target.position);
    }
}
