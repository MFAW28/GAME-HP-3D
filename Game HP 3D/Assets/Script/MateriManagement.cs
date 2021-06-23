using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MateriManagement : MonoBehaviour
{
    [SerializeField] private GameObject ExitGameUI;
    [SerializeField] private GameObject AllButtonUI;
    public GameObject CanvasMateriObj;
    public GameObject CanvasTutorial;

    public VideoPlayer MateriVP1;
    public VideoPlayer MateriVP2;
    public VideoPlayer MateriVP3;
    public VideoPlayer MateriVP4;
    public VideoPlayer MateriVP5;
    public VideoPlayer MateriVP6;
    public VideoPlayer MateriVP7;


    [Space(10)]
    [Header("EnemySpawn Settings")]
    public GameObject EnemyPrefabs;
    public Vector3 minPos;
    public Vector3 maxPos;
    private float x_axis;
    private float y_axis;
    private float z_axis;

    private LoadLevel animLoadLevel;
    private GameManagement GMGame;

    private void Start()
    {
        GameManagement.GameTutorial = true;
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        AllButtonUI.SetActive(true);
        ExitGameUI.SetActive(false);
        CanvasMateriObj.SetActive(true);

        MateriVP1.Stop();
        MateriVP2.Stop();
        MateriVP3.Stop();
        MateriVP4.Stop();
        MateriVP5.Stop();
        MateriVP6.Stop();
        MateriVP7.Stop();

        animLoadLevel = FindObjectOfType<LoadLevel>();
        GMGame = FindObjectOfType<GameManagement>();
    }

    private void Update()
    {
        GameManagement.GameTutorial = true;
        FindObjectOfType<AudioManager>().StopPlay("SoundGame");
        if (GMGame.countEnemy < 1)
        {
            SpawnEnemy();
        }

        if (GameManagement.GameIsPaused)
        {
            CanvasTutorial.SetActive(false);
        }
        else
        {
            CanvasTutorial.SetActive(true);
        }
    }

    private void SpawnEnemy()
    {
        x_axis = Random.Range(minPos.x, maxPos.x);
        y_axis = Random.Range(minPos.y, maxPos.y);
        z_axis = Random.Range(minPos.z, maxPos.z);
        Vector3 RandomPos = new Vector3(x_axis, y_axis, z_axis);

        Instantiate(EnemyPrefabs, RandomPos, Quaternion.identity);
    }

    public void ExitBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<Player>().offMove();
        ExitGameUI.SetActive(true);
        AllButtonUI.SetActive(false);
        CanvasMateriObj.SetActive(false);
        CanvasTutorial.SetActive(false);
    }
    public void ResumeGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        ExitGameUI.SetActive(false);
        AllButtonUI.SetActive(true);
        CanvasMateriObj.SetActive(true);
        CanvasTutorial.SetActive(true);
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        StartCoroutine(ExitGameAnim());
    }

    IEnumerator ExitGameAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenChestBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        GameManagement.GameEnd = false;
        GameManagement.GameIsStarted = false;
    }
}
