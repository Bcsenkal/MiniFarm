using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;


public class SettingsPanel : MonoBehaviour
{
    private Button saveButton;
    private Button quitButton;
    private Slider musicSlider;
    private Slider sfxSlider;
    private bool isOpened;
    void Start()
    {
        musicSlider = transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Slider>();
        sfxSlider = transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Slider>();
        saveButton = transform.GetChild(2).GetComponent<Button>();
        quitButton = transform.GetChild(3).GetComponent<Button>();
        saveButton.onClick.AddListener(SaveSettings);
        quitButton.onClick.AddListener(QuitGame);
        musicSlider.value = ES3.Load("musicVolume",1f);
        sfxSlider.value = ES3.Load("sfxVolume",1f);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);

        Managers.EventManager.Instance.OnOpenSettingsPanel += OpenSettings;
        Managers.EventManager.Instance.OnClick += CloseOnOuterClick;
    }

    private void OpenSettings()
    {
        
        OpenSettingsPanel().Forget();
    }

    private async UniTaskVoid OpenSettingsPanel()
    {
        await LMotion.Create(Vector3.zero, Vector3.one, 0.5f).WithEase(Ease.OutBack).BindToLocalScale(transform);
        musicSlider.interactable = true;
        sfxSlider.interactable = true;
        saveButton.interactable = true;
        quitButton.interactable = true;
        isOpened = true;
    }

    private async UniTaskVoid CloseSettingsPanel()
    {
        isOpened = false;
        musicSlider.interactable = false;
        sfxSlider.interactable = false;
        saveButton.interactable = false;
        quitButton.interactable = false;
        await LMotion.Create(Vector3.one, Vector3.zero, 0.5f).WithEase(Ease.InBack).BindToLocalScale(transform);
        Managers.EventManager.Instance.ONOnCloseSettingsPanel();
    }

    private void SaveSettings()
    {
        CloseSettingsPanel().Forget();
    }

    private void SetMusicVolume(float volume)
    {
        Managers.AudioManager.Instance.SetMusicVolume(volume);
    }
    private void SetSFXVolume(float volume)
    {
        Managers.AudioManager.Instance.SetSFXVolume(volume);
    }

    void OnApplicationQuit()
    {
        ES3.Save("musicVolume", musicSlider.value);
        ES3.Save("sfxVolume", sfxSlider.value);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void CloseOnOuterClick(IClickable clickable)
    {
        if(!isOpened) return;
        CloseSettingsPanel().Forget();
    }

    
}
