using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawn : MonoBehaviour
{

    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private float SpawnTime = 30f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SpawnEnemy), SpawnTime);
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, this.transform);
    }
}
