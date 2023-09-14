using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyprefab;

    public float maxX, minX;
    public float maxZ, minZ;

    int noOfWaves = 5;
    int numberOfEnemiesToSpawn = 2;
    int currentWave=1;
    int timeBetweenWaves = 5;

    public TextMeshProUGUI wavetext;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(2.0f);

        for (int wave = 1;wave<=noOfWaves;wave++)  //using for loop until it reaches the no of waves (5)
        {
            wavetext.text = "WAVE " + currentWave + "/" + noOfWaves.ToString(); //for ttext

            for (int i = 0; i< numberOfEnemiesToSpawn;i++) //to how many enemies to spawn
            {
                SpawnEnemy();
                yield return new WaitForSeconds(2.0f); 
            }

            yield return new WaitForSeconds(timeBetweenWaves);

            currentWave++;  
            numberOfEnemiesToSpawn += 5;//making the number of enemies great after every wave!

            Debug.Log(numberOfEnemiesToSpawn); //--------formytesting---------//
            Debug.Log(currentWave);
        }

    }
    void SpawnEnemy()
    {
        float randPosX = Random.Range(minX, maxX);
        float randPosZ = Random.Range(minZ, maxZ);
        Vector3 spawnPos = new Vector3(randPosX, transform.position.y, randPosZ);
        Instantiate(enemyprefab, spawnPos, Quaternion.identity);
    }
}
