using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Transform groundCheck;
    public Transform cameraTransform;


    CharacterController controller;

    public LayerMask groundMask;
    public LayerMask elevatorMask;

    Vector3 velocity;

    public float mouseSensitivity;
    float speed = 5f;
    float jumpHeight = 1.5f;
    float gravity = -9.81f;
    float groundDistance = 0.4f;
    float xRotation = 0f;

    bool isGrounded;
    public bool inElevator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        MoveCamera();
        MoveCharacter();
        Jump();

        inElevator = Physics.CheckSphere(groundCheck.position, groundDistance, elevatorMask);
    }

    void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    
    void MoveCharacter()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
