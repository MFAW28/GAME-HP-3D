using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private GameManagement GMGame;

    public bool TimeSpawnEnemy;
    public GameObject EnemyPrefabs;
    public float CountEnemyMax;
    public float maxEnemy;

    public GameObject ChestPrefabs;
    public float timeToSpawnChest;
    public Vector3 SizeBoxCollision;

    [SerializeField] private Vector3 center;
    [SerializeField] private Vector3 sizeBox;
    
    private Vector3 RandomPosition;
    
    private void Start()
    {
        GMGame = FindObjectOfType<GameManagement>();
        TimeSpawnEnemy = true;
        timeToSpawnChest = 5;
        maxEnemy = CountEnemyMax;
    }

    private void Update()
    {
        if (GameManagement.GameIsStarted == false && GameManagement.GameIsPaused == false && GameManagement.GameEnd == false)
        {
            if (GMGame.countEnemy < maxEnemy && TimeSpawnEnemy)
            {
                SpawnEnemy();
            }
            if (GMGame.countEnemy == maxEnemy)
            {
                TimeSpawnEnemy = false;
            }

            if (GMGame.countChest < 1)
            {
                if(timeToSpawnChest < 0)
                {
                    SpawnChestPlace();
                }
                else
                {
                    timeToSpawnChest -= Time.deltaTime;
                }
            }
        }
    }

    void SpawnEnemy()
    {
        RandomPosition = center + new Vector3(Random.Range(-sizeBox.x / 2, sizeBox.x / 2),
            sizeBox.y / 2,
            Random.Range(-sizeBox.z / 2, sizeBox.z / 2));
        if (!Physics.CheckBox(RandomPosition, SizeBoxCollision))
        {
            Instantiate(EnemyPrefabs, RandomPosition, Quaternion.identity);
        }
        else
        {
            SpawnEnemy();
        }
    }

    private void SpawnChestPlace()
    {
        RandomPosition = center + new Vector3(Random.Range(-sizeBox.x / 2, sizeBox.x / 2),
            sizeBox.y / 2,
            Random.Range(-sizeBox.z / 2, sizeBox.z / 2));
        if (!Physics.CheckBox(RandomPosition, SizeBoxCollision))
        {
            Instantiate(ChestPrefabs, RandomPosition, Quaternion.identity);
            GMGame.countChest += 1;
            timeToSpawnChest = 5;
        }
        else
        {
            SpawnChestPlace();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, sizeBox);
    }

    public void ResetChest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<ChestScript>().DestroyChest();
        FindObjectOfType<QuizManagement>().ResetJawabanBenar();
        GMGame.countChest -= 1;
    }
}
