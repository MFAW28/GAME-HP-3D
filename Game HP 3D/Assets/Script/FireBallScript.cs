using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    private ScoreController scoreGame;
    [SerializeField] private GameObject DestroyBulletMage;

    private void Start()
    {
        scoreGame = FindObjectOfType<ScoreController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("MageHit");
            DestroyObject();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            DestroyObject();
        }
    }
    private void DestroyObject()
    {
        GameObject effectDestroy = Instantiate(DestroyBulletMage, transform.position, transform.rotation);
        Destroy(effectDestroy, 2f);
        Destroy(gameObject);
    }
}
