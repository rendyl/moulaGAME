using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTerrainController : MonoBehaviour
{
    public AudioSource audioPlayerINV;
    public AudioSource mySource;
    public float timeBeforeMute = 0f;
    public float timeToMute;

    // Start is called before the first frame update
    void Start()
    {
        mySource.mute = false;
        timeToMute = 1f;
        timeBeforeMute = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeBeforeMute += Time.deltaTime;
        if (audioPlayerINV.isPlaying)
        {
            if(mySource.mute == false)
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
            timeBeforeMute = 0f;
            mySource.volume = Mathf.Lerp(mySource.volume, 0.1f, Time.deltaTime * 5);
        }
    }
}
