using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animPlayer;
    private Player playerScript;
    private WeaponsPlayer weaponsScript;
    private QuizSelect scoreGame;
    public int intFireBall;

    //attackSwordPlayer
    private float timeBtwSwordAttack = 1.2f;
    private float timeSwordAttack;
    private bool SwordAttackAnimation;
    //ColliderSword
    [SerializeField] private Collider colliderSword;
    //Trail Renderer
    [SerializeField] private TrailRenderer[] trailSword;

    //mageSwordAttack
    private float timeBtwMageAttack = 1.2f;
    private float timeMageAttack;
    private bool MageAttackAnimation;
    [SerializeField] private Transform FireMagePosition;
    [SerializeField] private GameObject FireMageEffect;
    [SerializeField] private GameObject fireBallPrefabs;
    private float speedFireBall = 18f;

    //gun Attack
    private float timeBtwGunAttack = 1.2f;
    private float timeGunAttack;
    private bool GunAttack;
    private float speedFireBulletGun = 50f;
    [SerializeField] private Transform firePointGun;
    [SerializeField] private Rigidbody bulletGunPrefabs;

    private void Awake()
    {
        animPlayer = this.GetComponent<Animator>();
        playerScript = this.GetComponent<Player>();
        weaponsScript = this.GetComponent<WeaponsPlayer>();
        scoreGame = this.GetComponent<QuizSelect>();
        timeSwordAttack = timeBtwSwordAttack;
        timeMageAttack = timeBtwMageAttack;
    }

    void Start()
    {
        this.intFireBall = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Player Attack
        if (weaponsScript.WeaponsReady)
        {
            //Sword Attack
            if (Input.GetKey(KeyCode.T) && SwordAttackAnimation == false)
            {
                SwordAttackAnimation = true;
            }else if(Input.GetKeyUp(KeyCode.T)&&SwordAttackAnimation == true)
            {
                SwordAttackAnimation = false;
            }

            if (SwordAttackAnimation)
            {
                if (weaponsScript.swordHealth >= 5)
                {
                    animPlayer.SetBool("AttackSword", true);
                }
                else
                {
                    colliderSword.enabled = false;
                    trailSword[0].enabled = false;
                    trailSword[1].enabled = false;
                    animPlayer.SetBool("AttackSword", false);
                }
            }
            else
            {
                animPlayer.SetBool("AttackSword", false);
            }

            //Mage Attack
            if (Input.GetKey(KeyCode.T) && MageAttackAnimation == false)
            {
                MageAttackAnimation = true;
                timeMageAttack = timeBtwMageAttack;
            }
            if (timeMageAttack <= 0)
            {
                MageAttackAnimation = false;
            }
            else
            {
                timeMageAttack -= Time.deltaTime;
            }

            if (MageAttackAnimation)
            {
                animPlayer.SetBool("AttackMage", true);
            }
            else
            {
                animPlayer.SetBool("AttackMage", false);
            }

            //Gun Attack
            timeGunAttack -= Time.deltaTime;
            if (Input.GetKey(KeyCode.T) && GunAttack == true && weaponsScript.GunReady == true)
            {
                FireGun();
                timeGunAttack = timeBtwGunAttack;
            }
            if(timeGunAttack < 0)
            {
                GunAttack = true;
            }
            else
            {
                GunAttack = false;
            }
        }
        else
        {
            CancelInvoke("forAttackBtnLoL");
        }
    }

    public void onAttackBtn()
    {
        playerScript.attackView = true;
        if(weaponsScript.SwordReady || weaponsScript.StaffMageReady)
        {
            StartCoroutine(offTheMovePlayer1());
            playerScript.attackStopMove = true;
        }
        if (weaponsScript.SwordReady)
        {
            InvokeRepeating("forAttackBtnLoL", 0f, 0f);
        }
        if(weaponsScript.StaffMageReady || weaponsScript.GunReady)
        {
            InvokeRepeating("forAttackBtnLoL", 0f, 1.2f);
        }
    }

    IEnumerator offTheMovePlayer1()
    {
        playerScript.attackStopMove = true;
        colliderSword.enabled = true;
        trailSword[0].enabled = true;
        trailSword[1].enabled = true;
        yield return new WaitForSeconds(1.1f);
        colliderSword.enabled = false;
        trailSword[0].enabled = false;
        trailSword[1].enabled = false;
        if (playerScript.attackView)
        {
            StartCoroutine(offTheMovePlayer2());
        }
        else
        {
            playerScript.attackStopMove = false;
        }
    }

    IEnumerator offTheMovePlayer2()
    {
        playerScript.attackStopMove = true;
        colliderSword.enabled = true;
        trailSword[0].enabled = true;
        trailSword[1].enabled = true;
        yield return new WaitForSeconds(1.1f);
        colliderSword.enabled = false;
        trailSword[0].enabled = false;
        trailSword[1].enabled = false;
        if (playerScript.attackView)
        {
            StartCoroutine(offTheMovePlayer1());
        }
        else
        {
            playerScript.attackStopMove = false;
        }
    }

    public void offAttackBtn()
    {
        playerScript.attackView = false;
        if (SwordAttackAnimation)
        {
            SwordAttackAnimation = false;
        }
        CancelInvoke("forAttackBtnLoL");
    }

    IEnumerator MagetoFire()
    {
        GameObject effectFire = Instantiate(FireMageEffect, new Vector3 (FireMagePosition.position.x, 2.5f, FireMagePosition.position.z), FireMagePosition.rotation);
        effectFire.transform.parent = transform;
        yield return new WaitForSeconds(.5f);
        Destroy(effectFire);
        if (weaponsScript.staffMageHealth >= 5)
        {
            FindObjectOfType<AudioManager>().Play("MageShoot");
            GameObject FireBullet = Instantiate(fireBallPrefabs, new Vector3(FireMagePosition.position.x, 2.5f, FireMagePosition.position.z), FireMagePosition.rotation);
            Rigidbody rbFireBullet = FireBullet.GetComponent<Rigidbody>();
            weaponsScript.staffMageHealth -= 5;
            rbFireBullet.AddForce(transform.forward * speedFireBall, ForceMode.Impulse);
        }
    }

    private void forAttackBtnLoL()
    {
        if (SwordAttackAnimation == false && weaponsScript.SwordReady == true)
        {
            SwordAttackAnimation = true;
        }
        if (MageAttackAnimation == false && weaponsScript.StaffMageReady == true)
        {
            StartCoroutine(MagetoFire());
            timeMageAttack = timeBtwMageAttack;
            MageAttackAnimation = true;
        }
        if (GunAttack == true && weaponsScript.GunReady == true)
        {
            if (weaponsScript.GunHealth >= 5)
            {
                FireGun();
                weaponsScript.GunHealth -= 5;
            }
            timeGunAttack = timeBtwGunAttack;
        }
    }

    private void FireGun()
    {
        animPlayer.SetBool("AttackGun", true);
        FindObjectOfType<AudioManager>().Play("GunShoot");
        Rigidbody rbbullet = Instantiate(bulletGunPrefabs, new Vector3 (firePointGun.position.x, 2.5f,firePointGun.position.z), firePointGun.transform.rotation);
        rbbullet.AddForce(transform.forward * speedFireBulletGun, ForceMode.Impulse);
        animPlayer.SetBool("AttackGun", false);
    }

    public void AttackSwordSound()
    {
        FindObjectOfType<AudioManager>().Play("SwordAttack");
    }
}
