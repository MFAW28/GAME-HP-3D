using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordScript : MonoBehaviour
{
    [SerializeField] private WeaponsPlayer weaponsScript;

    private void Start()
    {
        weaponsScript = this.gameObject.GetComponentInParent<WeaponsPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("SwordHit");
            weaponsScript.swordHealth -= 5;
        }
    }
}
