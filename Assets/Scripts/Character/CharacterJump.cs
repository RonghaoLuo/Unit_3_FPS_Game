using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Vector3 currentMotion;
    [SerializeField] private Vector3 gravity;
    [SerializeField] private Vector3 baseJumpForce = new(0, 20, 0);
    [SerializeField] private float jumpForceMultiplier = 1.0f;

    private Vector3 JumpForce => baseJumpForce * jumpForceMultiplier;
    private float powerUpTimer = 0f;

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

            if (currentMotion.y < gravity.y)
            {
                currentMotion.y = gravity.y;
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

        #region Power up
        if (powerUpTimer > 0)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0)
            {
                jumpForceMultiplier = 1.0f;
            }
        }

        #endregion
    }

    public void Jump()
    {
        if (!isGrounded) return;
        currentMotion = JumpForce;
        isGrounded = false;
    }

    public void StartPowerUp(float jumpForceMult, float duration)
    {
        jumpForceMultiplier = jumpForceMult;
        powerUpTimer = duration;
    }
}
