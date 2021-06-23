using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextBackPembahasan : MonoBehaviour
{

    public GameObject Image1;
    public GameObject Image2;

    void Start()
    {
        Image1.SetActive(true);
        Image2.SetActive(false);
    }

    void Update()
    {
        
    }

    public void Next()
    {
        Image1.SetActive(false);
        Image2.SetActive(true);
    }

    public void back()
    {
        Image1.SetActive(true);
        Image2.SetActive(false);
    }
}
