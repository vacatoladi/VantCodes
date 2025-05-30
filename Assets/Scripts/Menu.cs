using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public PlayerController pC;

    public GameObject pauseMenu;
    public GameObject menu1;
    public GameObject menu2;

    public Scrollbar scrollbar;

    public TMP_InputField inputField;

    public TMP_Dropdown dropdownDisplayMode;
    public TMP_Dropdown dropdownResolution;

    int resolutionX = 1920;
    int resolutionY = 1080;

    public float mouseSensitivity = 500f;

    bool itsPaused = false;
    bool menu2State = false;
    bool fullScreen = true;

    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;

        scrollbar.value = mouseSensitivity / 1000f;

        inputField.text = $"{mouseSensitivity / 1000f}";

        pC.mouseSensitivity = mouseSensitivity;

        dropdownDisplayMode.value = 0;
        dropdownResolution.value = 0;
        Screen.SetResolution(resolutionX, resolutionY, fullScreen);
        
    } 

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!itsPaused)
            {
                Cursor.lockState = CursorLockMode.None;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;

            }

            if (itsPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;


                if (menu2State)
                {
                    menu1.SetActive(true);
                    menu2.SetActive(false);
                }
            }

            itsPaused = !itsPaused;

        }

    }

    public void Resuming()
    {

        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        itsPaused = !itsPaused;

    }

    public void Menu2(bool aberto)
    {

        menu2State = aberto;
        menu2.SetActive(true);
        menu1.SetActive(false);

    }

    public void Menu2Fechado(bool fechado)
    {

        menu2State = fechado;
        menu2.SetActive(false);
        menu1.SetActive(true);

    }

    public void ScrollSensitivity()
    {

        mouseSensitivity = scrollbar.value * 1000f;
        float value = mouseSensitivity / 1000f;
        inputField.text = value.ToString("F3");

    }

    public void InputSensitivity()
    {

        string value = inputField.text;
        mouseSensitivity = float.Parse(value) * 1000f;
        scrollbar.value = mouseSensitivity / 1000f;

    }

    public void DisplayMode()
    {

        int value = dropdownDisplayMode.value;

        switch (value)
        {

            case 0:
                fullScreen = true;
                dropdownResolution.interactable = true;
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(resolutionX, resolutionY, fullScreen);
                break;

            case 1:
                fullScreen = false;
                dropdownResolution.interactable = true;
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                Screen.SetResolution(resolutionX, resolutionY, fullScreen);
                break;

            case 2:
                fullScreen = true;
                resolutionX = 1920;
                resolutionY = 1080;
                dropdownResolution.interactable = false;
                dropdownResolution.value = 0;
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Screen.SetResolution(resolutionX, resolutionY, fullScreen);
                break;

        }

    }

    public void Resolution()
    {
        switch (dropdownResolution.value)
        {

            case 0:
                resolutionX = 1920;
                resolutionY = 1080;
                break;

            case 1:
                resolutionX = 1768;
                resolutionY = 922;
                break;

            case 2:
                resolutionX = 1600;
                resolutionY = 900;
                break;

            case 3:
                resolutionX = 1360;
                resolutionY = 768;
                break;

            case 4:
                resolutionX = 1280;
                resolutionY = 720;
                break;

        }

        Screen.SetResolution(resolutionX, resolutionY, fullScreen);

    }

    public void Restart()
    {
        //Vai restartar a missão
        Debug.Log("Nenhuma missão ativa");
    }

    public void quit()
    {
        Application.Quit();
    }

}
