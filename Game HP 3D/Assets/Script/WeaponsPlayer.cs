using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPlayer : MonoBehaviour
{
    [Header("DoorUI")]
    //Get the door -------------------------------------------------------
    [SerializeField] private GameObject doorBtnUI;
    [SerializeField] private GameObject reachChest;
    [SerializeField] private GameObject doorBtnEnd;
    private QuizManagement quizManagement;

    [Space(10)]
    [Header("for Weapons")]
    //get the chest-------------------------------------------------------------------
    [SerializeField] private float RaycastDistance = 25f;
    [SerializeField] private GameObject attackBtnUI;
    [SerializeField] private GameObject pickWeaponsUI;
    [SerializeField] private Transform raycastPosition;

    //get the Weapons
    public bool WeaponsReady;
    [SerializeField] private GameObject HealthBarWeapons;
    [SerializeField] private GameObject effectDestroyWeapons;

    [Space (10)]
    [Header ("Sword Settings")]
    //pick the Sword------------------------------------------------------------------
    [SerializeField] private GameObject Sword;
    [SerializeField] private Sprite SwordBtnImage;
    public bool SwordReady;
    private int swordMaxHealth = 80;
    public int swordHealth;
    [SerializeField] private Sprite SwordHealthImage;

    [Space(10)]
    [Header("Mage Settings")]
    //pick the staff mage --------------------------------------------------
    [SerializeField] private GameObject StaffMage;
    [SerializeField] private Sprite StaffMageBtnImage;
    public bool StaffMageReady;
    private int staffMageMaxHealth = 40;
    public int staffMageHealth;
    [SerializeField] private Sprite StaffMageHealthImage;

    [Space(10)]
    [Header("Gun Settings")]
    //pick the Gun-------------------------------------------------
    [SerializeField] private GameObject Gun;
    [SerializeField] private Sprite GunBtnImage;
    public bool GunReady;
    private int GunMaxHealth = 60;
    public int GunHealth;
    [SerializeField] private Sprite GunHealthImage;

    [Space(10)]
    //access component player------------------------------------------------------------
    [SerializeField] private WeaponsPlayerUI weaponsBarUI;
    private Animator animPlayer2;
    private Player scriptPlayer;
    private PlayerAttack playerAttack;
    private camLookButton camLook;

    //boolean for UI
    [SerializeField] private GameObject openChestBtn;
    [SerializeField] private GameObject allBtn;
    private bool hitChest;

    [SerializeField] private Image attackBtnImage;
    [SerializeField] private RectTransform AttackBtnHandle;
    [SerializeField] private Image AttackHealthImage;

    private ChestScript chestScript;

    float timersword = 1;
    float timerMage = 1;
    float timerGun = 1;

    private void Start()
    {
        HealthBarWeapons.SetActive(false);
        pickWeaponsUI.SetActive(false);
        attackBtnUI.SetActive(false);
        openChestBtn.SetActive(false);
        allBtn.SetActive(true);
        reachChest.SetActive(true);
        SwordReady = false;
        StaffMageReady = false;
        GunReady = false;
        hitChest = false;
        animPlayer2 = this.GetComponent<Animator>();
        scriptPlayer = this.GetComponent<Player>();
        playerAttack = this.GetComponent<PlayerAttack>();
        quizManagement = FindObjectOfType<QuizManagement>();
        camLook = this.gameObject.GetComponentInChildren<camLookButton>();

        //health weapons
        swordHealth = swordMaxHealth;
        staffMageHealth = staffMageMaxHealth;
        GunHealth = GunMaxHealth;
    }

    void Update()
    {
        if (GameManagement.GameTutorial)
        {
            RaycastHit hitWeaponsTutorial;
            if (Physics.Raycast(raycastPosition.transform.position, raycastPosition.transform.TransformDirection(Vector3.forward), out hitWeaponsTutorial, 5f))
            {
                if (hitWeaponsTutorial.collider.tag == "Chest")
                {
                    hitChest = true;
                    chestScript = hitWeaponsTutorial.transform.gameObject.GetComponent<ChestScript>();
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        allBtn.SetActive(false);
                        pickWeaponsUI.SetActive(true);
                    }
                }
                else
                {
                    hitChest = false;
                }
            }
            else
            {
                hitChest = false;
            }
        }


        if (!GameManagement.GameTutorial && GameManagement.GameIsStarted)
        {
            doorBtnUI.SetActive(true);
        }

        if (!GameManagement.GameIsStarted)
        {
            RaycastHit hitWeapons;
            if (Physics.Raycast(raycastPosition.transform.position, raycastPosition.transform.TransformDirection(Vector3.forward), out hitWeapons, 5f))
            {
                if (hitWeapons.collider.tag == "Chest")
                {
                    hitChest = true;
                    chestScript = hitWeapons.transform.gameObject.GetComponent<ChestScript>();
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        allBtn.SetActive(false);
                        pickWeaponsUI.SetActive(true);
                    }
                }
                else
                {
                    hitChest = false;
                }

                if (hitWeapons.collider.tag == "EndDoor")
                {
                    if (quizManagement.jawabanBenar > 4)
                    {
                        doorBtnEnd.SetActive(true);
                    }
                    else
                    {
                        reachChest.SetActive(true);
                    }
                }
                else
                {
                    doorBtnEnd.SetActive(false);
                }

            }
            else
            {
                hitChest = false;
                doorBtnUI.SetActive(false);
                doorBtnEnd.SetActive(false);
                reachChest.SetActive(false);
            }
        }

        
        if (hitChest)
        {
            openChestBtn.SetActive(true);
        }
        else
        {
            openChestBtn.SetActive(false);
        }

        changeWeapons();

        if (WeaponsReady)
        {
            hitChest = false;
            scriptPlayer.bringWeaponView = true;
            HealthBarWeapons.SetActive(true);
            attackBtnUI.SetActive(true);
        }
        else
        {
            HealthBarWeapons.SetActive(false);
            scriptPlayer.bringWeaponView = false;
            scriptPlayer.attackView = false;
            attackBtnUI.SetActive(false);
        }

        //Health Weapons
        if (swordHealth < swordMaxHealth)
        {
            if (timersword < 0)
            {
                swordHealth += 1;
                timersword = 1;
            }
            else
            {
                timersword -= Time.deltaTime;
            }
        }
        if (staffMageHealth < staffMageMaxHealth)
        {
            if(timerMage < 0)
            {
                staffMageHealth += 1;
                timerMage = 1;
            }
            else
            {
                timerMage -= Time.deltaTime;
            }
        }
        if (GunHealth < GunMaxHealth)
        {
            if (timerGun < 0)
            {
                GunHealth += 1;
                timerGun = 1;
            }
            else
            {
                timerGun -= Time.deltaTime;
            }
        }

        if(GameManagement.GameIsPaused){
            allBtn.SetActive(false);
        }else{
            allBtn.SetActive(true);
        }
        if(GameManagement.GameEnd){
            allBtn.SetActive(false);
        }
    }

    //change Weapons
    void changeWeapons()
    {
        if (SwordReady)
        {
            Sword.SetActive(true);
            animPlayer2.SetLayerWeight(1, 1f);

            weaponsBarUI.SetWeapons(swordHealth);
            attackBtnImage.sprite = SwordBtnImage;
            AttackHealthImage.sprite = SwordHealthImage;
        }
        else
        {
            Sword.SetActive(false);
            animPlayer2.SetLayerWeight(1, 0f);
        }

        if (StaffMageReady)
        {
            StaffMage.SetActive(true);
            animPlayer2.SetLayerWeight(2, 1f);

            weaponsBarUI.SetWeapons(staffMageHealth);
            attackBtnImage.sprite = StaffMageBtnImage;
            AttackHealthImage.sprite = StaffMageHealthImage;
        }
        else
        {
            StaffMage.SetActive(false);
            animPlayer2.SetLayerWeight(2, 0f);
        }

        if (GunReady)
        {
            Gun.SetActive(true);
            animPlayer2.SetLayerWeight(3, 1f);
            animPlayer2.SetLayerWeight(4, 1f);

            weaponsBarUI.SetWeapons(GunHealth);
            attackBtnImage.sprite = GunBtnImage;
            AttackHealthImage.sprite = GunHealthImage;
        }
        else
        {
            Gun.SetActive(false);
            animPlayer2.SetLayerWeight(3, 0f);
            animPlayer2.SetLayerWeight(4, 0f);
        }
    }

    // Button
    public void openChest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        openChestBtn.SetActive(false);
        pickWeaponsUI.SetActive(true);
        GameManagement.GameIsPaused = true;
    }

    public void closeChest()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        pickWeaponsUI.SetActive(false);
        if (!GameManagement.GameIsStarted)
        {
            chestScript.DestroyChest();
            forUIButton();
        }
    }

    public void forUIButton()
    {
        GameManagement.GameIsPaused = false;
    }

    public void pickSwordBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        weaponsBarUI.SetMaxWeapons(swordMaxHealth);

        WeaponsReady = true;
        SwordReady = true;
        StaffMageReady = false;
        GunReady = false;
    }

    public void pickStaffMageBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        weaponsBarUI.SetMaxWeapons(staffMageMaxHealth);

        WeaponsReady = true;
        SwordReady = false;
        StaffMageReady = true;
        GunReady = false;
    }

    public void pickGunBtn()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        weaponsBarUI.SetMaxWeapons(GunMaxHealth);

        WeaponsReady = true;
        SwordReady = false;
        StaffMageReady = false;
        GunReady = true;
    }

    public void RunFast()
    {
        WeaponsReady = false;
        SwordReady = false;
        StaffMageReady = false;
        GunReady = false;
    }
}
