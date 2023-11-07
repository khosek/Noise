using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float noiseDecayRate;
    [SerializeField] float noise;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;

    IEnumerator SpawnZombie() {
        while (true)
        {
            // Spawn in the enemy
            GameObject newEnemy = GameObject.Instantiate(enemy);
            int randIndex = Random.Range(0, spawnPoints.Length);
            newEnemy.transform.position = spawnPoints[randIndex].position;

            // Wait for an amount of time based on the current noise level
            float waitTime = 2.2f - (noise / 50f);
            yield return new WaitForSeconds(waitTime);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NoiseController.instance.Sound.AddListener(changeNoise);
        StartCoroutine(SpawnZombie());
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
