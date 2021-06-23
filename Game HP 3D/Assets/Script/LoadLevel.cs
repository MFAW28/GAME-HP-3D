using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public Animator animLevel;

    void Awake()
    {
        animLevel = GetComponent<Animator>();
    }
}
