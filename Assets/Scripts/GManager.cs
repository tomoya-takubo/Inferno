using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    public GAMESTATES gameStates = GAMESTATES.OPENING;

    public AudioClip whistle; // ホイッスル音

    public enum GAMESTATES
    {
        OPENING,
        PLAYING,
        GAMEOVER,
        RESULTING,
        RESULTED
    }

    void Awake()
    {
        // シングルトン
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOverWhistle()
    {
        this.GetComponent<AudioSource>().PlayOneShot(whistle);
    }
}
