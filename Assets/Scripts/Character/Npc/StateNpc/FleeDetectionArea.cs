using UnityEngine;

public class FleeDetectionArea : MonoBehaviour
{
    [SerializeField] private Transform eyeOrigin;
    [SerializeField] private bool canSeeTarget;
    private WanderingNpc characterAI;

    private void Awake()
    {
        characterAI = GetComponentInParent<WanderingNpc>();
    }

    private void OnTriggerExit(Collider other)
    {
        characterAI.ChangeState(new IdleNpcState(characterAI));
        canSeeTarget = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (canSeeTarget == false)
        {
            RaycastHit hitInfo = new RaycastHit();
            if (!other.TryGetComponent<CharacterController>(out var character))
            {
                return;
            }

            Vector3 directionToPlayer = (character.transform.position + character.center) - eyeOrigin.position;

            bool hasHit = Physics.Raycast(eyeOrigin.position, directionToPlayer, out hitInfo);

            if (hasHit)
            {
                Debug.Log("Flee Detection Raycast is hitting " + hitInfo.collider.name);
            }

            if (hasHit && hitInfo.collider.transform == character.transform)
            {
                canSeeTarget = true;
                FleeFromTarget(character.transform);
            }
        }
    }

    private void FleeFromTarget(Transform target)
    {
        FleeNpcState fleeState = new(characterAI);
        fleeState.target = target;

        characterAI.ChangeState(fleeState);
    }
}
