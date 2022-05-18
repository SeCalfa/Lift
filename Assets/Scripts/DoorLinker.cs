using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLinker : MonoBehaviour
{

    [SerializeField]
    private GameManager gameManager;


    public void DoorOpened()
    {
        gameManager.GetLift.liftState = Lift.LiftState.DoorOpened;
    }

    public void DoorClosed()
    {
        if (GetComponent<Animator>().GetBool("IsLiftButtonPressed"))
            gameManager.GetLift.liftState = Lift.LiftState.Ride;
        else
        {
            gameManager.GetLift.liftState = Lift.LiftState.Stay;
            gameManager.GetComponent<SoundManager>().AudioMute(true);
        }
    }


    public GameManager GetGameManager { get { return gameManager; } }

}
