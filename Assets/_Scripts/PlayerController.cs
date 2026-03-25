using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    private PlayerInputActions inputActions;

    private Player playerInstance;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void Start()
    {
        playerInstance = Player.instance;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0);
        transform.position += move * playerInstance.MoveSpeed * Time.deltaTime;
    }
}
