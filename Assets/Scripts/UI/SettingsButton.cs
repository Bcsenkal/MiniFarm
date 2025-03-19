using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SettingsButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenSettings);
        Managers.EventManager.Instance.OnCloseSettingsPanel += EnableButton;
    }

    private void OpenSettings()
    {
        button.interactable = false;
        Managers.EventManager.Instance.ONOnOpenSettingsPanel();
    }

    private void EnableButton()
    {
        button.interactable = true;
    }
}
