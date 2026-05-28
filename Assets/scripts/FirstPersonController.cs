using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float crouchSpeed = 2.5f; 
    public float lookSensitivity = 2f;
    public float gravity = -9.81f; 

    [Header("Crouch Settings")]
    public float standingHeight = 2f; 
    public float crouchHeight = 1f; 

    [Header("link")]
    public Transform playerCamera;
    
    [Header("Animation")]
    public Animator animator;
    public string speedParameterName = "Speed";
    public string crouchParameterName = "IsCrouching"; 

    private CharacterController controller;
    private float cameraPitch = 0f;
    private Vector3 velocity; 
    
    private bool isCrouching = false;
    private Vector3 cameraStandPos;
    
    // НОВЕ: Запам'ятовуємо стандартний центр капсули
    private Vector3 characterCenterStand; 

    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        // Зберігаємо оригінальний центр (зазвичай Y = 0)
        characterCenterStand = controller.center; 

        if (playerCamera != null)
        {
            cameraStandPos = playerCamera.localPosition; 
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return; 
        }

        // --- ПОВОРОТ КАМЕРИ ---
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // --- ПРИСІДАННЯ ---
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
        }

        // 1. Плавно міняємо висоту
        float targetHeight = isCrouching ? crouchHeight : standingHeight;
        controller.height = Mathf.Lerp(controller.height, targetHeight, Time.deltaTime * 10f);

        // 2. НОВЕ: Плавно міняємо центр капсули, щоб п'ятки залишалися на місці!
        float heightDifference = standingHeight - targetHeight;
        Vector3 targetCenter = new Vector3(characterCenterStand.x, characterCenterStand.y - (heightDifference / 2f), characterCenterStand.z);
        controller.center = Vector3.Lerp(controller.center, targetCenter, Time.deltaTime * 10f);

        // 3. Плавно опускаємо камеру
        if (playerCamera != null)
        {
            float heightOffset = (standingHeight - targetHeight) / 2f;
            Vector3 targetCamPos = new Vector3(cameraStandPos.x, cameraStandPos.y - heightOffset, cameraStandPos.z);
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, targetCamPos, Time.deltaTime * 10f);
        }

        // --- ХОДЬБА ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        float currentMoveSpeed = isCrouching ? crouchSpeed : walkSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentMoveSpeed * Time.deltaTime);

        // --- ГРАВІТАЦІЯ ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- АНІМАЦІЯ ---
        if (animator != null)
        {
            float currentSpeed = new Vector2(moveX, moveZ).magnitude;
            animator.SetFloat(speedParameterName, currentSpeed);
            animator.SetBool(crouchParameterName, isCrouching);
        }
    }
}