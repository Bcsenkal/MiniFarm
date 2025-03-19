using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.SetMusicVolume(ES3.Load("musicVolume",1f));
        AudioManager.Instance.SetSFXVolume(ES3.Load("sfxVolume",1f));
        AudioManager.Instance.PlayMusic(true);
    }
}
