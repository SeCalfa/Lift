using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftFloorDetector : MonoBehaviour
{

    [SerializeField]
    private Lift lift;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 8)
        {
            lift.GetSetCurrentFloor = other.GetComponent<LiftPoint>().GetFloor;
        }
    }

}
