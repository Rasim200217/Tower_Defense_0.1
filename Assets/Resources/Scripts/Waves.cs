using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waves : MonoBehaviour
{
    public SpawnerScript spawnerLeft;
    public SpawnerScript spawnerRight;

    public Wave[] waves;

    private int _currentWave;
    public Text waveText;

    private void Start()
    {
        _currentWave = Manager.startWave;
        waveText.text = "Waves: " + (_currentWave + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) StartBattle();
    }

    public void StartBattle()
    {
        if (Manager.gameLost || Manager.gameWin)
        {
            Manager.Restart();
            return;
        }
        
        if (Manager.waveStarted) return;

        Manager.waveStarted = true;
        Manager.enemiesAlive = GetEnemiesCount();

        spawnerLeft.StartSpawn(waves[_currentWave]);
        spawnerRight.StartSpawn(waves[_currentWave]);
        
        _currentWave++;
        waveText.text = "Waves: " + _currentWave; 

        if (_currentWave >= waves.Length)
        {
            Manager.finalWave = true;
            Debug.Log("Final");
        }
    }

    private int GetEnemiesCount()
    {
        Wave wave = waves[_currentWave];

        int temp = 0;
        for (int i = 0; i < wave.packs.Length; i++)
        {
            temp += wave.packs[i].count;
        }
       return temp * 2;
    }
}
