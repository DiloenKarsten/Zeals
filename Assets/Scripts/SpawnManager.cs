using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Enemy> enemyPrefabs;
    public GameObject gameArea;
    private int enemyCount;
    private float spawnRange;
    // Start is called before the first frame update
    void Start()
    {
        spawnRange = gameArea.GetComponent<BoxCollider>().size.x*2;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount<3)
        {
            Instantiate(enemyPrefabs[0], GenerateSpawnPoint(), enemyPrefabs[0].transform.rotation);
        }
        
    }

    private Vector3 GenerateSpawnPoint()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 1.5f, spawnPosZ);
        return randomPos;
    }
}
