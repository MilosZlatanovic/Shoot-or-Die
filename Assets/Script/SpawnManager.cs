using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    private bool _stopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, positionSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);

            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], positionSpawn, Quaternion.identity);
           
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
        }
    }
    public void OnPlyerDeath()
    {
        _stopSpawning = true;
    }
}
