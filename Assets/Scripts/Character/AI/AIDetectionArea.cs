using UnityEngine;

public class AIDetectionArea : MonoBehaviour
{
    [SerializeField] private Transform eyeOrigin;
    [SerializeField] private bool canSeeTarget;
    private CharacterAIChasePlayer characterAI;

    private void Awake()
    {
        characterAI = GetComponentInParent<CharacterAIChasePlayer>();
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
            CharacterController character = other.GetComponent<CharacterController>();

            if (character == null)
            {
                return;
            }

            Vector3 directionToPlayer = (character.transform.position + character.center) - eyeOrigin.position;

            bool hasHit = Physics.Raycast(eyeOrigin.position, directionToPlayer, out hitInfo);

            if (hasHit)
            {
                Debug.Log("Raycast is hitting " + hitInfo.collider.name);
            }

            if (hasHit && hitInfo.collider.transform == character.transform)
            {
                canSeeTarget = true;
                ChaseTarget(character.transform);
            }
        }
    }

    private void ChaseTarget(Transform target)
    {
        ChaseNpcState chaseState = new ChaseNpcState(characterAI);
        chaseState.targetToChase = target;

        characterAI.ChangeState(chaseState);
    }
}
