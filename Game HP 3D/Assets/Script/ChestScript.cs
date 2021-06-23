using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public void DestroyChest()
    {
        FindObjectOfType<AudioManager>().Play("Mati");
        Destroy(gameObject);
    }
}
