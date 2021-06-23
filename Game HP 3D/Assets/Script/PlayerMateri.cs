using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMateri : MonoBehaviour
{
    [SerializeField] private Transform raycastPosition;
    [SerializeField] private GameObject AllButton;
    private bool raycastDraw;

    [SerializeField] private GameObject Materi1;
    [SerializeField] private GameObject btnMateri1;

    [SerializeField] private GameObject Materi2;
    [SerializeField] private GameObject btnMateri2;

    [SerializeField] private GameObject Materi3;
    [SerializeField] private GameObject btnMateri3;

    [SerializeField] private GameObject Materi4;
    [SerializeField] private GameObject btnMateri4;

    [SerializeField] private GameObject Materi5;
    [SerializeField] private GameObject btnMateri5;

    [SerializeField] private GameObject Materi6;
    [SerializeField] private GameObject btnMateri6;

    [SerializeField] private GameObject Materi7;
    [SerializeField] private GameObject btnMateri7;

    void Start()
    {
        AllButton.SetActive(true);
        raycastDraw = true;

        Materi1.SetActive(false);
        btnMateri1.SetActive(false);
        Materi2.SetActive(false);
        btnMateri2.SetActive(false);
        Materi3.SetActive(false);
        btnMateri3.SetActive(false);
        Materi4.SetActive(false);
        btnMateri4.SetActive(false);
        Materi5.SetActive(false);
        btnMateri5.SetActive(false);
        Materi6.SetActive(false);
        btnMateri6.SetActive(false);
        Materi7.SetActive(false);
        btnMateri7.SetActive(false);
    }

    void FixedUpdate()
    {
        if (raycastDraw)
        {
            RaycastHit hitWeapons;
            if (Physics.Raycast(raycastPosition.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hitWeapons, 5f))
            {
                if (!GameManagement.GameIsPaused)
                {
                    if (hitWeapons.collider.tag == "Materi1")
                    {
                        btnMateri1.SetActive(true);
                    }
                    else
                    {
                        btnMateri1.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi2")
                    {
                        btnMateri2.SetActive(true);
                    }
                    else
                    {
                        btnMateri2.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi3")
                    {
                        btnMateri3.SetActive(true);
                    }
                    else
                    {
                        btnMateri3.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi4")
                    {
                        btnMateri4.SetActive(true);
                    }
                    else
                    {
                        btnMateri4.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi5")
                    {
                        btnMateri5.SetActive(true);
                    }
                    else
                    {
                        btnMateri5.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi6")
                    {
                        btnMateri6.SetActive(true);
                    }
                    else
                    {
                        btnMateri6.SetActive(false);
                    }
                    if (hitWeapons.collider.tag == "Materi7")
                    {
                        btnMateri7.SetActive(true);
                    }
                    else
                    {
                        btnMateri7.SetActive(false);
                    }
                }
                else
                {
                    btnMateri1.SetActive(false);
                    btnMateri2.SetActive(false);
                    btnMateri3.SetActive(false);
                    btnMateri4.SetActive(false);
                    btnMateri5.SetActive(false);
                    btnMateri6.SetActive(false);
                    btnMateri7.SetActive(false);
                }
            }
            else
            {
                btnMateri1.SetActive(false);
                btnMateri2.SetActive(false);
                btnMateri3.SetActive(false);
                btnMateri4.SetActive(false);
                btnMateri5.SetActive(false);
                btnMateri6.SetActive(false);
                btnMateri7.SetActive(false);
            }
        }
    }

    public void BtnMateri1()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi1.SetActive(true);
        raycastDraw = false;
        btnMateri1.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri2()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi2.SetActive(true);
        raycastDraw = false;
        btnMateri2.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri3()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi3.SetActive(true);
        raycastDraw = false;
        btnMateri3.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri4()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi4.SetActive(true);
        raycastDraw = false;
        btnMateri4.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri5()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi5.SetActive(true);
        raycastDraw = false;
        btnMateri5.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri6()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi6.SetActive(true);
        raycastDraw = false;
        btnMateri6.SetActive(false);
        AllButton.SetActive(false);
    }

    public void BtnMateri7()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().StopPlay("SoundPembukaan");
        GameManagement.GameIsPaused = true;
        Materi7.SetActive(true);
        raycastDraw = false;
        btnMateri7.SetActive(false);
        AllButton.SetActive(false);
    }

    public void CloseMateri()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        FindObjectOfType<AudioManager>().Play("SoundPembukaan");
        GameManagement.GameIsPaused = false;
        raycastDraw = true;
        Materi1.SetActive(false);
        Materi2.SetActive(false);
        Materi3.SetActive(false);
        Materi4.SetActive(false);
        Materi5.SetActive(false);
        Materi6.SetActive(false);
        Materi7.SetActive(false);
        AllButton.SetActive(true);
    }
}
