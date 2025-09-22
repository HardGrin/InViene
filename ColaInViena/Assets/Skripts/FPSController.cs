using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Mouse Look")]
    public Transform playerCamera;
    public float mouseSensitivity = 2f;
    public float verticalClamp = 85f;
    [Range(0.01f, 1f)] public float lookSmoothness = 0.1f; // ��������� ������

    [Header("Head Bobbing")]
    public bool enableHeadBob = true;
    public float bobFrequency = 6f;
    public float bobAmplitude = 0.05f;
    private float bobTimer = 0f;
    private Vector3 camDefaultPos;

    private Rigidbody rb;
    private float xRotation = 0f;
    private bool isGrounded;

    // ��� �������� ��������
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 targetMouseDelta = Vector2.zero;
    private Vector2 mouseSmoothVelocity = Vector2.zero;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ����� �� �������� �� ��������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerCamera != null)
            camDefaultPos = playerCamera.localPosition;
    }

    void Update()
    {
        HandleMouseLook();
        HandleHeadBob();
        HandleJump();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        targetMouseDelta = new Vector2(mouseX, mouseY);

        // ������� ����������� ����
        currentMouseDelta.x = Mathf.SmoothDamp(currentMouseDelta.x, targetMouseDelta.x, ref mouseSmoothVelocity.x, lookSmoothness);
        currentMouseDelta.y = Mathf.SmoothDamp(currentMouseDelta.y, targetMouseDelta.y, ref mouseSmoothVelocity.y, lookSmoothness);

        // ������� ������ �� ���������
        xRotation -= currentMouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * currentMouseDelta.x);
    }

    void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * h + transform.forward * v).normalized;
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void HandleHeadBob()
    {
        if (!enableHeadBob || playerCamera == null) return;

        if (rb.linearVelocity.magnitude > 0.1f && isGrounded)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobOffset = Mathf.Sin(bobTimer) * bobAmplitude;
            playerCamera.localPosition = camDefaultPos + new Vector3(0, bobOffset, 0);
        }
        else
        {
            bobTimer = 0f;
            playerCamera.localPosition = Vector3.Lerp(playerCamera.localPosition, camDefaultPos, Time.deltaTime * 5f);
        }
    }
}
