using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class HeroController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float gravityValue = -9.1f;

    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;


    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start()
    {
        isGrounded = true;
        controller = GetComponent<CharacterController>();
        //inputManager = GetComponent<InputManager>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movement = inputManager.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0f, movement.y);

        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;

        controller.Move(move * Time.deltaTime * moveSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        if (inputManager.PlayerJumped() && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpSpeed * (-3.0f) * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }
}
