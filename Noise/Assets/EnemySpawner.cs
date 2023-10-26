using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NoiseController.instance.Sound.AddListener(changeNoise);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void changeNoise(float sound) 
    {
        Debug.Log("Sound receieved: " + sound);
    }
}
