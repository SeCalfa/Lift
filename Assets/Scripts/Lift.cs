using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lift : MonoBehaviour
{

    internal LiftState liftState = LiftState.Stay;

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Fotoelement fotoelement;
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    [Range(0.1f, 1)]
    private float liftSpeed;
    [SerializeField]
    private Text currentFloorText;
    [SerializeField]
    private ButtonInLift currentButtonLift; // default value in inspector on start

    private int currentFloor;
    private Vector3 startLiftPos;
    private Vector3 targetLiftPos;
    private bool isLastButtonInLift = false;
    private float alpha = 0;
    private int floorCount = 0;
    private ButtonPanelFloor currentButtonFloor;

    public enum LiftState
    {
        Stay,
        Ride,
        DoorOpening,
        DoorOpened,
        DoorClosing
    }

    private void Update()
    {
        SetCurrentFloorText();
        LiftMove();
    }

    private void SetCurrentFloorText()
    {
        currentFloorText.text = gameManager.GetLift.GetSetCurrentFloor.ToString();
    }

    private void LiftMove()
    {
        if (liftState == LiftState.Ride)
        {
            alpha = Mathf.Clamp(alpha + Time.deltaTime * liftSpeed / floorCount, 0, 1);
            transform.position = Vector3.Lerp(startLiftPos, targetLiftPos, alpha);
            gameManager.GetComponent<SoundManager>().AudioMute(false);

            if (alpha == 1)
            {
                if (!isLastButtonInLift)
                    currentButtonFloor.FloorDoorOpen();
                else
                    currentButtonLift.GetButtonPanelFloor.FloorDoorOpen();

                leftDoor.GetComponent<Animator>().SetTrigger("OpenLDlift");
                rightDoor.GetComponent<Animator>().SetTrigger("OpenRDlift");
                leftDoor.GetComponent<Animator>().SetBool("IsLiftButtonPressed", false);
            }
        }
    }

    public void LiftMoveSettingsReset(int targetFloor, bool isInLift)
    {
        floorCount = Mathf.Abs(targetFloor - currentFloor);
        alpha = 0;

        if (!isInLift)
        {
            liftState = LiftState.Ride;
            return;
        }

        if (liftState == LiftState.Stay)
            liftState = LiftState.Ride;
    }

    public void OpenLiftDoor()
    {
        leftDoor.GetComponent<Animator>().SetTrigger("OpenLDlift");
        rightDoor.GetComponent<Animator>().SetTrigger("OpenRDlift");
    }

    public void CloseLiftDoor()
    {
        leftDoor.GetComponent<Animator>().SetTrigger("Close");
        rightDoor.GetComponent<Animator>().SetTrigger("Close");
    }



    // Properties
    public GameObject GetLeftDoor { get { return leftDoor; } }
    public bool GetSetIsLastButtonInLift
    {
        get { return isLastButtonInLift; }
        set { isLastButtonInLift = value; }
    }
    public int GetSetCurrentFloor
    {
        get { return currentFloor; }
        set { currentFloor = value; }
    }
    public Vector3 GetSetStartLiftPos
    {
        get { return startLiftPos; }
        set
        {
            if (liftState == LiftState.Stay || liftState == LiftState.DoorClosing || liftState == LiftState.DoorOpened)
                startLiftPos = value;
        }
    }
    public Vector3 GetSetTargetLiftPos
    {
        get { return targetLiftPos; }
        set
        {
            if (liftState == LiftState.Stay || liftState == LiftState.DoorClosing || liftState == LiftState.DoorOpened)
                targetLiftPos = value;
        }
    }
    public ButtonPanelFloor GetSetCurrentButtonFloor
    {
        get { return currentButtonFloor; }
        set { currentButtonFloor = value; }
    }
    public ButtonInLift GetSetCurrentButtonInLift
    {
        get { return currentButtonLift; }
        set { currentButtonLift = value; }
    }

}