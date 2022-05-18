using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fotoelement : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;

    private float time = 0;

    private void Update()
    {
        TimeToDoorClose();
    }

    private void OnTriggerEnter(Collider other)
    {
        print(gameManager.GetLift.liftState);
        if (other.tag == "Player" && gameManager.GetLift.liftState == Lift.LiftState.Stay)
        {
            gameManager.GetLift.OpenLiftDoor();
            if (!gameManager.GetLift.GetSetIsLastButtonInLift)
                gameManager.GetLift.GetSetCurrentButtonFloor.FloorDoorOpen();
            else
                gameManager.GetLift.GetSetCurrentButtonInLift.GetButtonPanelFloor.FloorDoorOpen();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            time = 0;
        }
    }

    private void TimeToDoorClose()
    {
        if(gameManager.GetLift.liftState == Lift.LiftState.DoorOpened)
        {
            time += Time.deltaTime;
            if (time >= 3.0f)
            {
                gameManager.GetLift.CloseLiftDoor();

                if (gameManager.GetLift.GetSetIsLastButtonInLift)
                    gameManager.GetLift.GetSetCurrentButtonFloor.FloorDoorClose();
                else
                    gameManager.GetLift.GetSetCurrentButtonInLift.GetButtonPanelFloor.FloorDoorClose();

                time = 0f;
                print(gameManager.GetLift.GetSetCurrentButtonFloor.transform.parent.name + " | " + gameManager.GetLift.GetSetCurrentButtonInLift.GetButtonPanelFloor.transform.parent.name);
            }
        }
    }

}
