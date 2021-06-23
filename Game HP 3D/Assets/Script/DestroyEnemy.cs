using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private GameManagement GM;
    //Dead Effect
    [SerializeField] private GameObject deadEffectEnemy;


    [SerializeField] private Renderer renderEnemyStu;
    [SerializeField] private Color[] colorPick;

    private void Start()
    {
        GM = FindObjectOfType<GameManagement>();
        GM.countEnemy += 1;

        int NumColor = Random.Range(0, colorPick.Length);
        renderEnemyStu.material.color = colorPick[NumColor];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            GameObject effectDead = Instantiate(deadEffectEnemy, transform.position, Quaternion.identity);
            Destroy(effectDead, 1.5f);
            enemyDestroy();
        }
        if (other.gameObject.CompareTag("FireBallMage"))
        {
            GameObject effectDead = Instantiate(deadEffectEnemy, transform.position, Quaternion.identity);
            Destroy(effectDead, 1.5f);
            enemyDestroy();
        }
        if (other.gameObject.CompareTag("BulletGun"))
        {
            GameObject effectDead = Instantiate(deadEffectEnemy, transform.position, Quaternion.identity);
            Destroy(effectDead, 1.5f);
            enemyDestroy();
        }
    }

    void enemyDestroy()
    {
        FindObjectOfType<AudioManager>().Play("Mati");
        GM.countEnemy -= 1;
        Destroy(gameObject);
    }
}
