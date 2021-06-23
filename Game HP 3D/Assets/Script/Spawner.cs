using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    private GameManagement GMGame;

    public GameObject EnemyPrefabs;
    public float minTimeSpawnEnemy;
    public float maxTimeSpawnEnemy;
    private float timeToSpawn;
    private float maxEnemy;

    public GameObject ChestPrefabs;
    public Vector3 SizeBoxCollision;

    public bool Level1;
    [SerializeField] private Vector3 center1;
    [SerializeField] private Vector3 sizeBox1;

    public bool Level2;
    [SerializeField] private Vector3 center2;
    [SerializeField] private Vector3 sizeBox2;

    public bool Level3;
    [SerializeField] private Vector3 center3;
    [SerializeField] private Vector3 sizeBox3;

    public bool Level4;
    [SerializeField] private Vector3 center4;
    [SerializeField] private Vector3 sizeBox4;

    private Vector3 center;
    private Vector3 sizeBox;
    private Vector3 RandomPosition;
    
    private void Start()
    {
        GMGame = FindObjectOfType<GameManagement>();

        Level1 = false;
        Level2 = false;
        Level3 = false;
        Level4 = false;
        timeToSpawn = Random.Range(minTimeSpawnEnemy, maxTimeSpawnEnemy);
    }

    private void Update()
    {
        if (GameManagement.GameIsStarted == false && GameManagement.GameIsPaused == false && GameManagement.GameEnd == false)
        {
            if (GMGame.countEnemy < maxEnemy)
            {
                if (timeToSpawn < 0)
                {
                    SpawnEnemy();
                }

                else
                {
                    timeToSpawn -= Time.deltaTime;
                }
            }
        }

        SpawnPlace();
    }

    void SpawnEnemy()
    {
        RandomPosition = center + new Vector3(Random.Range(-sizeBox.x / 2, sizeBox.x / 2),
            sizeBox.y / 2,
            Random.Range(-sizeBox.z / 2, sizeBox.z / 2));
        if (!Physics.CheckBox(RandomPosition, SizeBoxCollision))
        {
            Instantiate(EnemyPrefabs, RandomPosition, Quaternion.identity);
            timeToSpawn = Random.Range(minTimeSpawnEnemy, maxTimeSpawnEnemy);
        }
        else
        {
            SpawnEnemy();
        }
    }

    void SpawnPlace()
    {
        if (Level1 == true)
        {
            maxEnemy = 4;
            center = center1;
            sizeBox = sizeBox1;
        }
        if (Level2 == true)
        {
            maxEnemy = 6;
            center = center2;
            sizeBox = sizeBox2;
        }
        if (Level3 == true)
        {
            maxEnemy = 8;
            center = center3;
            sizeBox = sizeBox3;
        }
        if (Level4 == true)
        {
            maxEnemy = 10;
            center = center4;
            sizeBox = sizeBox4;
        }
    }

    public void SpawnChest()
    {
        StartCoroutine(SpawnChestTime());
    }

    IEnumerator SpawnChestTime()
    {
        yield return new WaitForSeconds(5f);
        SpawnChestPlace();
    }

    private void SpawnChestPlace()
    {
        RandomPosition = center + new Vector3(Random.Range(-sizeBox.x / 2, sizeBox.x / 2),
            sizeBox.y / 2,
            Random.Range(-sizeBox.z / 2, sizeBox.z / 2));
        if (!Physics.CheckBox(RandomPosition, SizeBoxCollision))
        {
            Instantiate(ChestPrefabs, RandomPosition, Quaternion.identity);
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

    public void OpenDoor2()
    {
        Level1 = false;
        Level2 = true;
        Level3 = false;
        Level4 = false;
        ResetChest();
    }

    public void OpenDoor3()
    {
        Level1 = false;
        Level2 = false;
        Level3 = true;
        Level4 = false;
        ResetChest();
    }

    public void OpenDoor4()
    {
        Level1 = false;
        Level2 = false;
        Level3 = false;
        Level4 = true;
        ResetChest();
    }

    private void ResetChest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<ChestScript>().DestroyChest();
        FindObjectOfType<QuizManagement>().ResetJawabanBenar();
        SpawnChest();
    }
}
