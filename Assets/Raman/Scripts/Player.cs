using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun, IPunObservable
{
    public PhotonView pv;
    private CharacterController controller;
    private Vector3 playerVelocity;

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpSpeed = 10;
    [SerializeField] private float gravityValue = 10;

    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius;

    private Vector3 smoothMove;

    private InputManager inputManager;

    private void Start()
    {
        isGrounded = true;
        controller = GetComponent<CharacterController>();
        //inputManager = InputManager.Instance;
        Debug.Log(inputManager.gameObject);
    }

    private void Update()
    {
        //if (photonView.IsMine)
        //{
        HandleMovement();
        //}
        //else
        //{
        //    SmoothMovement();
        //}
    }

    private void SmoothMovement()
    {
        transform.position = Vector3.Lerp(transform.position, smoothMove, 0.1f);
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
        controller.Move(move * Time.deltaTime * moveSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (inputManager.PlayerJumped() && isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpSpeed * (-3.0f) * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            smoothMove = (Vector3)stream.ReceiveNext();
        }
    }
}
