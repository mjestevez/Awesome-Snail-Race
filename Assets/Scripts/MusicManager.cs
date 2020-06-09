using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class MusicManager : Singleton<MusicManager>
{
    public AudioClip menuClip;
    public AudioClip raceClip;
    private AudioSource aSr;
    private void Awake()
    {
        if (MusicManager.Instance != this) Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        aSr = GetComponent<AudioSource>();
    }

    public void LoadClip(string name)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" && name == "Race")
        {
            aSr.clip = raceClip;
            aSr.Play();
        }else if(SceneManager.GetActiveScene().name == "Race" && name == "MainMenu")
        {
            aSr.clip = menuClip;
            aSr.Play();
        }
    }
}
