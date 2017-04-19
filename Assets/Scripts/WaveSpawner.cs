﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour 
{
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    private float countdown = 2f;
    private int waveIndex = 0;

    public Text waveCountdownText;

	void Update () 
	{
        if (EnemiesAlive > 0)
        {
            return;
        }

	    if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());    // since ienumerator, we cant just call SpawnWave();
            countdown = timeBetweenWaves;
            return;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity); // this is so the countdown never goes below 0

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
	}

    IEnumerator SpawnWave() // ienumerator is available from System.Collection, allowed us to pause this piece of code for amount of seconds
    {
        PlayerStats.Rounds++;

        Wave wave = waves[waveIndex];

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
