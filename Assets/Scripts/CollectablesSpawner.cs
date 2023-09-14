using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] Prefabs;

    public float maxX, minX;
    public float maxZ, minZ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(9f);

        float randPosX = Random.Range(minX, maxX);
        float randPosZ = Random.Range(minZ, maxZ);

        Vector3 Pos = new Vector3(randPosX, transform.position.y, randPosZ);

        int randomPrefabIndex = Random.Range(0, Prefabs.Length);
        GameObject selectedPrefab = Prefabs[randomPrefabIndex];

        Instantiate(selectedPrefab, Pos, selectedPrefab.transform.rotation);

        yield return new WaitForSeconds(1f);  //takes total 10 sec later next object to spawn
        StartCoroutine(SpawnEnemies());

    }
}
