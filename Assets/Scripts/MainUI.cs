using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{

    [SerializeField]
    private Image aim;

    [Space][Space]
    [SerializeField]
    private Sprite aimNoAction;
    [SerializeField]
    private Sprite aimPrepareToAction;

    [Space][Space]
    [SerializeField]
    private GameObject tutorial;
    [SerializeField]
    private CanvasGroup menu;

    private bool isTotorialOn = true;
    private bool isMenuOn = false;

    public void PrepareToAction(bool isPreparing)
    {
        if (isPreparing)
            aim.sprite = aimPrepareToAction;
        else
            aim.sprite = aimNoAction;
    }

    public void MenuOn()
    {
        menu.alpha = 1;
        menu.interactable = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isMenuOn = true;
    }

    public void MenuOff()
    {
        menu.alpha = 0;
        menu.interactable = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isMenuOn = false;
    }

    public void TutorialOnOff()
    {
        if (isTotorialOn)
            tutorial.SetActive(false);
        else
            tutorial.SetActive(true);

        isTotorialOn = !isTotorialOn;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Lift");
    }

    public void QuitGame()
    {
        Application.Quit();
    }





    public bool GetIsMenuOn { get { return isMenuOn; } }

}
