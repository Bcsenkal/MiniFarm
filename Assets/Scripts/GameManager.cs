using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using LitMotion;
using LitMotion.Extensions;
using LitMotion.Adapters;
public class GameManager : MonoBehaviour
{
    void Awake()
    {
        //Litmotion causes a frame drop when it is first used, so we are preloading it here to avoid that.
        MotionDispatcher.EnsureStorageCapacity<Vector3, NoOptions, Vector3MotionAdapter>(10);
    }
    
    void Start()
    {

        AudioManager.Instance.SetMusicVolume(ES3.Load("musicVolume",1f));
        AudioManager.Instance.SetSFXVolume(ES3.Load("sfxVolume",1f));
        AudioManager.Instance.PlayMusic(true);
    }
}
