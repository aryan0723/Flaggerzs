using Photon.Pun;
using UnityEngine;
public class PlayerController : MonoBehaviourPunCallbacks, IDamagable
{
    [Header("Move Variable")]
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    [Header("Gravity Variable")]
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float groundDistance;

    [SerializeField] private float gravity;
    [SerializeField] private bool isCharacterGrounded = false;
    private Vector3 velocity = Vector3.zero;

    [Header("Gun")]
    [SerializeField] private SingleShotGun gun;

    [Header("Animation")]
    private Animator anim;

    private PhotonView pv;

    const float maxHealth = 3f;
    float currentHealth;


    PlayerManager playerManager;

    private void Start()
    {
        currentHealth = maxHealth;

        GetReference();
        InitVariable();
        pv = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)pv.InstantiationData[0]).GetComponent<PlayerManager>();

        if (!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

    }
    private void Update()
    {
        if (!pv.IsMine) return;

        HandleIsGrounded();
        HandleJumping();

        HandleGravity();
        HandleRunning();
        HandleMovement();
        HandleShooting();
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Use();
        }
    }


    private void HandleAnimation()
    {
        if (moveDirection == Vector3.zero)
        {
            anim.SetFloat("speed", 0);
        }
        else if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 0.5f);
        }
        else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("speed", 1f);
        }
    }
    private void HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");// get axis causes some sliding ... 
        float moveZ = Input.GetAxisRaw("Vertical");// use of GetAxis cause unnesseary sliding due to smoothing..therefore we use get axis raw

        moveDirection = new Vector3(moveX, 0, moveZ).normalized;


        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);

    }
    private void HandleRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = walkSpeed;
        }
    }
    private void HandleIsGrounded()
    {
        isCharacterGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);

    }
    private void HandleGravity()
    {
        if (isCharacterGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isCharacterGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -2 * gravity);
        }
    }
    private void GetReference()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }
    private void InitVariable()
    {
        moveSpeed = walkSpeed;
    }


    public void TakeDamage(float damage)
    {
        pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RPC_TakeDamage(float damage)
    {
        if (!pv.IsMine) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }

    }

    private void Die()
    {
        playerManager.Die();
    }
}
