using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nukeEffectsManager : MonoBehaviour
{
    private bool actif;
    private float tempsActif;
    public float speedLight;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Light>().intensity = 0;
        GetComponent<ParticleSystem>().Stop();
        GetComponent<AudioSource>().Stop();
        tempsActif = 0.5f;
    }
    
    public void activer()
    {
        GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().Play();
        tempsActif = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (tempsActif < 0.5f)
        {   
            tempsActif += Time.deltaTime;
            GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 10.00f, Time.deltaTime * speedLight);
        }
        else
        {
            if (GetComponent<Light>().intensity > 0) GetComponent<Light>().intensity = Mathf.Lerp(GetComponent<Light>().intensity, 0.00f, Time.deltaTime * 1);
        }
    }
}
