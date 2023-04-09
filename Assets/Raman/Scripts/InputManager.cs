using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static InputManager instance;
    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }

    private PlayerInputs playerInputs;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        playerInputs = new PlayerInputs();
    }
    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    public Vector2 GetPlayerMovement()
    {
        return playerInputs.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return playerInputs.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerInputs.Player.Jump.triggered;
    }
}
