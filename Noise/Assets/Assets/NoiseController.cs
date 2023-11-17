using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoiseController : MonoBehaviour
{
    public static NoiseController instance;

    private void Awake()
    {
        instance = this;
    }

    public myFloatEvent Sound;
    [System.Serializable]
    public class myFloatEvent : UnityEvent<float> { }

    // Start is called before the first frame update
    void Start()
    {
        if (Sound == null)
        {
            Sound = new myFloatEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float formatSound(int noiseChange, int maxNoise) 
    {
        return noiseChange + ((float)maxNoise) / 1000.0f;
    }
}
