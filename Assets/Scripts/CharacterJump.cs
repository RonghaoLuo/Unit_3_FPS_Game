using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Vector3 currentMotion;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private Vector3 jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            currentMotion += gravity * Time.deltaTime;

            if (currentMotion.y < -10f)
            {
                currentMotion.y = -10f;
            }
        }
        if (isGrounded)
        {
            if (currentMotion.y <= 0)
            {
                currentMotion.y = 0;
            }
        }
        

        controller.Move(currentMotion * Time.deltaTime);

        isGrounded = Physics.CheckSphere(controller.transform.position, 0.2f, groundLayer);
    }

    public void Jump()
    {
        if (!isGrounded) return;
        currentMotion = jumpForce;
        isGrounded = false;
    }
}
