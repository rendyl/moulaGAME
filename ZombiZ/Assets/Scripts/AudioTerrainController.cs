using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTerrainController : MonoBehaviour
{
    public AudioSource audioPlayerINV;
    public AudioSource nukeSource;
    public AudioSource mySource;
    public AudioSource roundSource;
    public PlayerController player;
    public float timeBeforeMute = 0f;
    public float timeToMute;
    public float sourceBaseVolume = 0.5f;
    public float timeRound = 0f;
    public bool played = false;

    // Start is called before the first frame update
    void Start()
    {
        mySource.volume = 0.00f;
        mySource.mute = false;
        timeToMute = 1f;
        timeBeforeMute = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(roundSource.isPlaying && timeRound < 9f)
        {
            mySource.pitch = Mathf.Lerp(mySource.pitch, 1f, Time.deltaTime * 7);
            timeRound += Time.deltaTime;
            if(audioPlayerINV.isPlaying) audioPlayerINV.volume = Mathf.Lerp(audioPlayerINV.volume, 0.1f, Time.deltaTime * 5);
            else mySource.volume = Mathf.Lerp(mySource.volume, 0.1f, Time.deltaTime * 5);
        }
        else if (roundSource.isPlaying && timeRound < 12f)
        {
            if (!played)
            {
               played = true;
               mySource.Play();
            }
            timeRound += Time.deltaTime;
            if (audioPlayerINV.isPlaying) audioPlayerINV.volume = Mathf.Lerp(audioPlayerINV.volume, 0.5f, Time.deltaTime);
            else mySource.volume = Mathf.Lerp(mySource.volume, sourceBaseVolume, Time.deltaTime);
        }
        else
        {
            timeRound = 0f;
            timeBeforeMute += Time.deltaTime;
            if (audioPlayerINV.isPlaying)
            {
                if (mySource.mute == false)
                {
                    if (timeBeforeMute < timeToMute)
                    {

                    }
                    else
                    {
                        mySource.volume = Mathf.Lerp(mySource.volume, 0.00f, Time.deltaTime * 5);
                    }
                }
            }
            else
            {
                if (player.rotated && !player.alive)
                {
                    mySource.pitch = Mathf.Lerp(mySource.pitch, 0.00f, Time.deltaTime);
                    if (mySource.pitch < 0.001) mySource.volume = Mathf.Lerp(mySource.volume, 0.00f, Time.deltaTime * 5);
                }
                else
                {
                    if (nukeSource.isPlaying)
                    {
                        mySource.pitch = Mathf.Lerp(mySource.pitch, 0.8f, Time.deltaTime * 5);
                        audioPlayerINV.pitch = Mathf.Lerp(audioPlayerINV.pitch, 0.8f, Time.deltaTime * 5);
                        roundSource.pitch = Mathf.Lerp(roundSource.pitch, 0.8f, Time.deltaTime * 5);
                    }
                    else
                    {
                        mySource.pitch = Mathf.Lerp(mySource.pitch, 1f, Time.deltaTime * 7);
                        audioPlayerINV.pitch = Mathf.Lerp(audioPlayerINV.pitch, 1f, Time.deltaTime * 7);
                        roundSource.pitch = Mathf.Lerp(roundSource.pitch, 1f, Time.deltaTime * 7);

                    }
                    timeBeforeMute = 0f;
                    mySource.volume = Mathf.Lerp(mySource.volume, sourceBaseVolume, Time.deltaTime * 5);
                }
            }
        }
    }
}
