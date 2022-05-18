using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanelFloor : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private Text currentFloorText;
    [SerializeField]
    private GameObject leftDoor;
    [SerializeField]
    private GameObject rightDoor;
    [SerializeField]
    private Transform liftPoint;
    [SerializeField]
    private int floor;

    private void Update()
    {
        SetCurrentFloorText();
    }

    private void SetCurrentFloorText()
    {
        currentFloorText.text = gameManager.GetLift.GetSetCurrentFloor.ToString();
    }

    public void FloorDoorOpen()
    {
        leftDoor.GetComponent<Animator>().SetTrigger("OpenLD");
        rightDoor.GetComponent<Animator>().SetTrigger("OpenRD");
    }

    public void FloorDoorClose()
    {
        leftDoor.GetComponent<Animator>().SetTrigger("Close");
        rightDoor.GetComponent<Animator>().SetTrigger("Close");
    }




    // Properties
    public int GetFloor { get { return floor; } }
    public Transform GetLiftPoint { get { return liftPoint; } }

}
