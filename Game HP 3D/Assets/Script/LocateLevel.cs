using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateLevel : MonoBehaviour
{
   
    public int Level;
    public int NextLevel;

    private void Awake()
    {
        GameManagement.GamePlay = true;
    }
}
