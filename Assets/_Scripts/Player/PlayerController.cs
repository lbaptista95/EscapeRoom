using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;
    [SerializeField] private float mouseSensivity;
    [SerializeField] private float playerSpeed;

    [SerializeField] private bool showCursor;

    private bool canMove;
 private Camera playerCam;
    private CharacterController controller;

    private float cameraPitch;

    private Vector2 mouseDelta = Vector2.zero;
    private Vector2 mouseDeltaVelocity = Vector2.zero;

    private Vector2 direction = Vector2.zero;
    private Vector2 directionVelocity = Vector2.zero;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        playerCam = GetComponentInChildren<Camera>();
    }

    public void SetMovePermission(bool state)
    {
        canMove = state;
        showCursor = !state;
        Cursor.visible = showCursor;
    }

    void Start()
    {
        canMove = true;
        playerCam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();

        Cursor.visible = showCursor;
    }

    void Update()
    {
        if (canMove)
        {
            CameraMovement();
            PlayerMovement();
        }
    }

    private void CameraMovement()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseDelta = Vector2.SmoothDamp(mouseDelta, targetMouseDelta, ref mouseDeltaVelocity, Time.deltaTime);

        cameraPitch -= mouseDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90, 90);
        playerCam.transform.localEulerAngles = Vector3.right * cameraPitch * mouseSensivity;
        transform.Rotate(Vector3.up, mouseDelta.x * mouseSensivity);
    }

    private void PlayerMovement()
    {
        Vector2 targetDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;        

        direction = Vector2.SmoothDamp(direction, targetDirection, ref directionVelocity, Time.deltaTime);

        Vector3 velocity = (transform.forward * direction.y + transform.right * direction.x) * playerSpeed;

        controller.Move(velocity);
    }
}
