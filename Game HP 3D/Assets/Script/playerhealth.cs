using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealth : MonoBehaviour
{
    [SerializeField] private int maxHealthPlayer = 100;
    public int healthPlayer;
    [SerializeField] private HealthPlayerUI HealthBar;

    [SerializeField] private GameObject deadEffectPlayer;

    void Start()
    {
        healthPlayer = maxHealthPlayer;
        HealthBar.SetMaxHealth(maxHealthPlayer);
    }

    void Update()
    {
        HealthBar.SetHealth(healthPlayer);
        if(healthPlayer > maxHealthPlayer)
        {
            healthPlayer = maxHealthPlayer;
        }

        if(healthPlayer <= 0)
        {
            GameManagement.GameEnd = true;
            GameManagement.GameLose = true;
            PlayerDeath();
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHand"))
        {
            FindObjectOfType<AudioManager>().Play("Mati");
            healthPlayer -= 5;
        }
    }
    void PlayerDeath()
    {
        GameObject effectDeath = Instantiate(deadEffectPlayer, transform.position, Quaternion.identity);
        Destroy(effectDeath, 0.3f);
    }
}
