using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTopDown : MonoBehaviour {

    public enum SpawnState { SPAWNING, WAITING, COUNTING};


	[System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }


    public Wave[] waves;
    private int nextWave = 0;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;
    public GameObject door;


    private float waveCountdown;
    private float searchCountdown = 1f;
    private SpawnState state = SpawnState.COUNTING;



	void Start ()
    {
        door.SetActive(false);

        waveCountdown = timeBetweenWaves;

        if (spawnPoints.Length == 0)
        {
            Debug.Log("No Spawn points referenced");
        }
    }
	



	void Update ()
    {

        if(nextWave > waves.Length)
        {
            Debug.Log("ifree");
        }

        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

		if(waveCountdown <= 0)
        {

            if (state != SpawnState.SPAWNING && nextWave < 3)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }

        else
        {
            waveCountdown -= Time.deltaTime;
        }

        EnemyIsAlive();
	}

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            //Change to whatever you want to happen AFTER waves are done
            door.SetActive(true);
            nextWave++;
            StopCoroutine(SpawnWave(waves[nextWave-1]));
            return;
            

        }
        else
        {
            nextWave++;
        }
    }


    bool EnemyIsAlive()
    {


        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            return false;
        }

        return true;

    }


    IEnumerator SpawnWave(Wave _wave)
    {
        while (nextWave < 4) {
            state = SpawnState.SPAWNING;

            for (int i = 0; i < _wave.count; i++)
            {
                SpawnEnemy(_wave.enemy);
                yield return new WaitForSeconds(1f / _wave.rate);
            }

            state = SpawnState.WAITING;


            yield break;
        }
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
       
    }

}

