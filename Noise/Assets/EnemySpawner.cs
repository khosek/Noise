using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float noiseDecayRate;
    [SerializeField] float noise;
    
    // Start is called before the first frame update
    void Start()
    {
        NoiseController.instance.Sound.AddListener(changeNoise);
    }

    // Update is called once per frame
    void Update()
    {
        if (noise > 0) { noise -= noiseDecayRate * Time.deltaTime; }
    }

    void changeNoise(float sound) 
    {
        Debug.Log("Sound receieved: " + sound);
        int maxNoise = (int) (sound * 100.0f) % 100;
        int noiseChange = (int) sound;
        if(noise < maxNoise - noiseChange) { noise += noiseChange; }
        else if (noise < maxNoise) { noise = maxNoise; }
        Debug.Log("Current noise: " + noise);
    }
}
