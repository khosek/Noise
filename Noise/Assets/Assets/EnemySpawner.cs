using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float noiseDecayRate;
    [SerializeField] float noise;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject enemy;

    [SerializeField] TextMeshProUGUI noiseText;

    [SerializeField] AudioSource[] songs;
    /*[SerializeField] AudioSource songLevel1;
    [SerializeField] AudioSource songLevel2;
    [SerializeField] AudioSource songLevel3;
    [SerializeField] AudioSource songLevel4;*/
    int previousSongLevel;
    int currentSongLevel;
    bool changingSong;

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

    IEnumerator changeVolumeLevel(AudioSource song, float start, float end)
    {
        float timeElapsed = 0.0f;
        while (timeElapsed < 2)
        {
            song.volume = Mathf.Lerp(start, end, timeElapsed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        song.volume = end;
        changingSong = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        NoiseController.instance.Sound.AddListener(changeNoise);
        StartCoroutine(SpawnZombie());
        currentSongLevel = 0;
        previousSongLevel = currentSongLevel;
        changingSong = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (noise > 0) { noise -= noiseDecayRate * Time.deltaTime; }
        if (noise < 0) { noise = 0; }
        noiseText.text = "Noise: " + (int) noise;

        /*songLevel1.volume = 1.0f - (noise / 25.0f);
        songLevel2.volume = getMin((noise - 25.0f) / 25.0f, 1.0f - ((noise - 25.0f) / 25.0f));
        songLevel3.volume = getMin((noise - 50.0f) / 25.0f, 1.0f - ((noise - 50.0f) / 25.0f));
        songLevel4.volume = (noise - 75.0f) / 25.0f;*/

        // Determining what song level should be playing
        if ((noise > (currentSongLevel + 1) * 25 || noise < currentSongLevel * 25) && !changingSong)
        {
            previousSongLevel = currentSongLevel;
            currentSongLevel = (int) (noise / 25.0f); 
            Debug.Log("Previous song level is " + previousSongLevel);
            Debug.Log("New song level is " + currentSongLevel);

            StartCoroutine(changeVolumeLevel(songs[previousSongLevel], 1.0f, 0.0f));
            StartCoroutine(changeVolumeLevel(songs[currentSongLevel], 0.0f, 1.0f));
            changingSong = true;
        }
    }

    void changeNoise(float sound) 
    {
        Debug.Log("Sound receieved: " + sound);
        int maxNoise = (int) (sound * 1000.0f) % 1000;
        int noiseChange = (int) sound;
        if(noise < maxNoise - noiseChange) { noise += noiseChange; }
        else if (noise < maxNoise) { noise = maxNoise; }
        Debug.Log("Current noise: " + noise);
    }
}
