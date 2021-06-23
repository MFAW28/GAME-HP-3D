using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGunScript : MonoBehaviour
{
    private ScoreController scoreGame;

    private void Start()
    {
        scoreGame = FindObjectOfType<ScoreController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            scoreGame.Score += 1;
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
