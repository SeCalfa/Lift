using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private MainUI mainUI;
    [SerializeField]
    private Lift lift;
    [SerializeField]
    private Player player;

    private void Awake()
    {
        SettingsOnStart();
    }

    private void SettingsOnStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }




    // Properties
    public MainUI GetMainUI { get { return mainUI; } }
    public Lift GetLift { get { return lift; } }
    public Player GetPlayer { get { return player; } }

}
