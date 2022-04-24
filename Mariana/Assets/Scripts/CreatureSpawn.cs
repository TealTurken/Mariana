using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject[] EnemySpawnList;
    [SerializeField]
    private float SpawnTime = 30f;
    private int randomNumber;

    void Start()
    {
        randomNumber = Random.Range(0, EnemySpawnList.Length);
        Invoke(nameof(SpawnEnemy), SpawnTime);
    }

    void SpawnEnemy()
    {
        Instantiate(EnemySpawnList[randomNumber], this.transform);
    }
}
