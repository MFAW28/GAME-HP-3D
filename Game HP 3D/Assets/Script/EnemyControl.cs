using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private GameManagement GM;
    [SerializeField] private Transform player;
    private NavMeshAgent agent;

    //starter
    private bool starterEnemy;
    [SerializeField] float timerFollow;

    public LayerMask whatIsPlayer;

    //attack player
    [SerializeField] private float timeBtwAttack;
    [SerializeField] private Collider attackCollider;

    //States
    [SerializeField] private float attackRange;
    private bool playerInAttackRange;

    //animation
    private Animator animEnemy;

    //Dead Effect
    [SerializeField] private GameObject deadEffectEnemy;

    [SerializeField] private Renderer renderEnemy;
    [SerializeField] private List<Color> colorPick;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animEnemy = GetComponent<Animator>();
        GM = FindObjectOfType<GameManagement>();
        GM.countEnemy += 1;
        starterEnemy = true;

        int NumColor = Random.Range(0, colorPick.Count);
        renderEnemy.material.color = colorPick[NumColor];
        StartCoroutine(EnemyStartFollow());
    }

    IEnumerator EnemyStartFollow()
    {
        yield return new WaitForSeconds(timerFollow);
        starterEnemy = false;
    }

    void Update()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (!starterEnemy)
        {
            if (GameManagement.GameIsPaused == false && GameManagement.GameEnd == false)
            {
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

                if (!playerInAttackRange) FollowPlayer();
                if (playerInAttackRange) AttackPlayer();
            }
            else
            {
                agent.SetDestination(transform.position);
            }
        }
        else
        {
            animEnemy.SetFloat("WalkZ", 0f);
        }
    }

    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
        animEnemy.SetFloat("WalkZ", 1f);
        animEnemy.SetBool("SwordAtt", false);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animEnemy.SetFloat("WalkZ", 0f);
        InvokeRepeating("AttackingEnemy", 1f, timeBtwAttack);
    }

    private void AttackingEnemy()
    {
        if (!GameManagement.GameIsPaused)
        {
            StartCoroutine(btwAttack());
        }
    }

    IEnumerator btwAttack()
    {
        animEnemy.SetBool("SwordAtt", true);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(.3f);
        attackCollider.enabled = false;
        animEnemy.SetBool("SwordAtt", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            FindObjectOfType<ScoreController>().Score += 2;
            GameObject effectDead = Instantiate(deadEffectEnemy, transform.position, Quaternion.identity);
            Destroy(effectDead, 1.5f);
            enemyDestroy();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            attackCollider.enabled = false;
        }
        if (other.gameObject.CompareTag("FireBallMage"))
        {
            FindObjectOfType<ScoreController>().Score += 5;
            GameObject effectDead = Instantiate(deadEffectEnemy, transform.position, Quaternion.identity);
            Destroy(effectDead, 1.5f);
            enemyDestroy();
        }
        if (other.gameObject.CompareTag("BulletGun"))
        {
            FindObjectOfType<ScoreController>().Score += 2;
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
