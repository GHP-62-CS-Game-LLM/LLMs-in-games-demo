using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(InteractionController))]

public class FirstPersonController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 5f;
    public float gravity = 10f;
    public float lookSpeed = 4f;
    public float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;
    public bool canLook = true;
    
    private CharacterController _characterController;
    private InteractionController _interactionController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _characterController.detectCollisions = true;
        _characterController.contactOffset = 0.5f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _interactionController = GetComponent<InteractionController>();
    }

    private void Update()
    {
        Move();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0) && Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void Move()
    {
        canMove = !_interactionController.IsInteracting;
        
        #region Movement

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float moveDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        #endregion

        #region Jump

        if (Input.GetButton("Jump") && canMove && _characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = moveDirectionY;
        }

        if (!_characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Rotate

        _characterController.Move(moveDirection * Time.deltaTime);

        if (canLook)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion
    }
}
