using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> monsterPrefabs;

    private List<GameObject> spawnedMonsters = new List<GameObject>();

    private void Start()
    {
        SpawnAllMonsters();
    }

    private void SpawnAllMonsters()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject spawnedMonster = SpawnMonster(spawnPoint.position, monsterPrefabs[0]);
            spawnedMonsters.Add(spawnedMonster);
        }
    }

    private GameObject SpawnMonster(Vector3 position, GameObject monsterPrefab)
    {
        GameObject spawnedMonster = Instantiate(monsterPrefab, position, Quaternion.identity);
        return spawnedMonster;
    }
}
