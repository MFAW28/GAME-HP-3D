using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SKKIKDManagement : MonoBehaviour
{
    private LoadLevel animLoadLevel;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        animLoadLevel = FindObjectOfType<LoadLevel>();
    }

    public void BackToMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        StartCoroutine(BackToMenuAnim());
    }

    IEnumerator BackToMenuAnim()
    {
        animLoadLevel.animLevel.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }
}
