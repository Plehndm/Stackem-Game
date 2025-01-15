using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    // [Header("Buttons")]
    // [SerializeField] private Button play;
    // [SerializeField] private Button options;
    [Header("Core Settings")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject playerHUD;
    [SerializeField] private GameObject uiBackground;

    [Header("Options Settings")]
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject volumeMenu;
    [SerializeField] private GameObject graphicsMenu;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;
    private EventSystem m_EventSystem;

    void Start()
    {
        m_EventSystem = EventSystem.current;

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        
        List<string> options = new();
        int resolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Insert(i, option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
                resolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        menu.SetActive(true);
    }

    public void PlayButton()
    {
        gameManager.StartGame();
        menu.SetActive(false);
        uiBackground.SetActive(false);
        playerHUD.SetActive(true);
    }

    public void OptionsButton()
    {
        menu.SetActive(false);
        optionsMenu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Volume"));
    }

    public void VolumeButton()
    {
        optionsMenu.SetActive(false);
        volumeMenu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Volume Slider"));
    }

    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        if(volumeSlider.value <= 0)
            muteToggle.isOn = true;
        else if(muteToggle.isOn)
            muteToggle.isOn = false;
    }

    public void SetMute()
    {
        volumeSlider.value = muteToggle.isOn ? 0 : 1;
        AudioListener.volume = muteToggle.isOn ? 0 : 1;
    }

    public void VolumeBack()
    {
        volumeMenu.SetActive(false);
        optionsMenu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Volume"));
    }

    public void GraphicsButton()
    {
        optionsMenu.SetActive(false);
        graphicsMenu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Resolution Dropdown"));
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void GraphicsApply()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        SetResolution(resolutionDropdown.value);
    }

    public void GraphicsBack()
    {
        graphicsMenu.SetActive(false);
        optionsMenu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Volume"));
    }

    public void BackButton()
    {
        optionsMenu.SetActive(false);
        menu.SetActive(true);
        m_EventSystem.SetSelectedGameObject(GameObject.Find("Start"));
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
