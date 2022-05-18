using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;

    [SerializeField]
    private GameManager gameManager;

    [Header("Movement properties")]
    [SerializeField]
    private float characterSpeed;
    [SerializeField]
    private float fallGravity;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundMask;
    
    [Header("Camera move properties")]
    [SerializeField]
    private float mouseSensitive;
    [SerializeField]
    private float minLookLimitY;
    [SerializeField]
    private float maxLookLimitY;

    [Header("Aim detector")]
    [SerializeField]
    private float raycastDistance;
    [SerializeField]
    private LayerMask aimHitLayer;

    // Movement
    private Vector3 velocity;
    private bool isGrounded = false;

    // Camera move
    private float rotationY = 0;

    // Aim detector
    private bool isAimPrepearedToAction = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
        CameraMovement();
        AimDetector();
        MenuActivity();
    }

    private void Movement()
    {
        if (gameManager.GetMainUI.GetIsMenuOn)
            return;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.5f, groundMask);

        if (isGrounded && velocity.y < 0)
            velocity.y = fallGravity;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 direction = transform.right * x + transform.forward * y;

        velocity.y += -100.8f * Time.deltaTime;

        controller.Move((direction + velocity) * characterSpeed * Time.deltaTime);
    }

    private void CameraMovement()
    {
        if (gameManager.GetMainUI.GetIsMenuOn)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitive * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitive * Time.deltaTime;

        rotationY -= mouseY;
        rotationY = Mathf.Clamp(rotationY, minLookLimitY, maxLookLimitY);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void AimDetector()
    {
        if (gameManager.GetMainUI.GetIsMenuOn)
            return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        
        isAimPrepearedToAction = false;
        if(Physics.Raycast(ray, out hit, raycastDistance, aimHitLayer, QueryTriggerInteraction.Ignore))
        {
            isAimPrepearedToAction = true;
        }

        gameManager.GetMainUI.PrepareToAction(isAimPrepearedToAction);

        if (Input.GetKeyDown(KeyCode.E) && isAimPrepearedToAction && gameManager.GetLift.liftState != Lift.LiftState.Ride)
        {
            if (gameManager.GetLift.GetSetCurrentButtonFloor == hit.collider.GetComponentInParent<ButtonPanelFloor>())
                return;

            // Buttons on floor
            if (hit.collider.gameObject.layer == 7)
            {
                gameManager.GetLift.GetSetIsLastButtonInLift = false;
                gameManager.GetLift.GetSetCurrentButtonFloor = hit.collider.GetComponentInParent<ButtonPanelFloor>();
                gameManager.GetLift.GetSetStartLiftPos = gameManager.GetLift.transform.position;
                gameManager.GetLift.GetSetTargetLiftPos = hit.collider.GetComponentInParent<ButtonPanelFloor>().GetLiftPoint.position;
                gameManager.GetLift.LiftMoveSettingsReset(hit.collider.GetComponentInParent<ButtonPanelFloor>().GetFloor, false);
            }
            // Buttons in lift
            else if (hit.collider.gameObject.layer == 9)
            {
                if (hit.collider.GetComponent<ButtonInLift>().GetFloorNumber == gameManager.GetLift.GetSetCurrentFloor)
                    return;

                gameManager.GetLift.GetSetIsLastButtonInLift = true;
                gameManager.GetLift.GetSetCurrentButtonInLift = hit.collider.GetComponent<ButtonInLift>();
                gameManager.GetLift.GetSetStartLiftPos = gameManager.GetLift.transform.position;
                gameManager.GetLift.GetSetTargetLiftPos = hit.collider.GetComponent<ButtonInLift>().GetButtonPanelFloor.GetLiftPoint.position;
                gameManager.GetLift.LiftMoveSettingsReset(hit.collider.GetComponent<ButtonInLift>().GetFloorNumber, true);
                gameManager.GetLift.GetLeftDoor.GetComponent<Animator>().SetBool("IsLiftButtonPressed", true);
            }
        }
    }

    private void MenuActivity()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            gameManager.GetMainUI.MenuOn();
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            gameManager.GetMainUI.MenuOff();
        }
    }

}
