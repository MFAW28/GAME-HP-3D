using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public int NumTut;

    public GameObject BtnTutorialUI;
    public GameObject Tut1;
    public GameObject Tut2;
    public GameObject Tut3;
    public GameObject Tut4;
    public GameObject Tut5;
    public GameObject Tut6;
    public GameObject EndTut;


    void Start()
    {
        NumTut = 1;
    }

    void Update()
    {
        if (!GameManagement.GameIsPaused)
        {
            ObjectActive();
            if (NumTut > 7)
            {
                NumTut = 0;
            }
        }
    }

    void ObjectActive()
    {
        if (NumTut == 0)
        {
            BtnTutorialUI.SetActive(true);
        }
        else
        {
            BtnTutorialUI.SetActive(false);
        }

        if (NumTut == 1)
        {
            Tut1.SetActive(true);
        }
        else
        {
            Tut1.SetActive(false);
        }
        if (NumTut == 2)
        {
            Tut2.SetActive(true);
        }
        else
        {
            Tut2.SetActive(false);
        }
        if (NumTut == 3)
        {
            Tut3.SetActive(true);
        }
        else
        {
            Tut3.SetActive(false);
        }
        if (NumTut == 4)
        {
            Tut4.SetActive(true);
        }
        else
        {
            Tut4.SetActive(false);
        }
        if (NumTut == 5)
        {
            Tut5.SetActive(true);
        }
        else
        {
            Tut5.SetActive(false);
        }
        if (NumTut == 6)
        {
            Tut6.SetActive(true);
        }
        else
        {
            Tut6.SetActive(false);
        }
        if (NumTut == 7)
        {
            EndTut.SetActive(true);
        }
        else
        {
            EndTut.SetActive(false);
        }
    }

    public void BtnTutorial()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        NumTut++;
    }

    public void BtnOpenTutorial()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        NumTut = 1;
    }
}
