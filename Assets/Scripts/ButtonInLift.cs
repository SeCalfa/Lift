using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInLift : MonoBehaviour
{

    [SerializeField]
    private int floorNumber;
    [SerializeField]
    private ButtonPanelFloor buttonPanelFloor;




    public int GetFloorNumber { get { return floorNumber; } }
    public ButtonPanelFloor GetButtonPanelFloor { get { return buttonPanelFloor; } }

}
