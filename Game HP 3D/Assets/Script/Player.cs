using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rigidBody;

    private CharacterController controller;
    private WeaponsPlayer weaponsPlayer;

    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float speed = 10f;
    [SerializeField] private float jumpHeight = 1.0f;
    private float gravityValue = -20f;

    [SerializeField] private Transform cam;
    [SerializeField] private float rotationSpeed = 4f;
    private float targetAngle;

    public bool bringWeaponView;
    public bool attackView;
    public bool attackStopMove;

    //animation
    private Animator animPlayer;
    private bool JumpAnimation;

    //Joystick and android needed
    [SerializeField] private Joystick joystick;
    private bool moveBtn;
    private bool RunBtn;

    //Effect Particle Move
    [SerializeField] private GameObject effectWalkPlayer;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        animPlayer = this.GetComponent<Animator>();
        weaponsPlayer = this.GetComponent<WeaponsPlayer>();
    }

    private void Awake()
    {
        bringWeaponView = false;
        attackView = false;
        attackStopMove = false;

        moveBtn = false;
        RunBtn = false;
        rigidBody = this.GetComponent<Rigidbody>();
        JumpAnimation = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (moveBtn && attackStopMove == false)
        {
            moveHorizontal = joystick.Horizontal;
            moveVertical = joystick.Vertical;
        }

        //Control speed Player
        if (attackStopMove)
        {
            speed = 0f;
        }
        if(!RunBtn && !attackStopMove)
        {
            if (weaponsPlayer.GunReady)
            {
                speed = 10f;
            }
            else
            {
                speed = 12f;
            }
        }

        if (RunBtn)
        {
            speed = 17f;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cam.forward * move.z + cam.right * move.x;
        move.y = 0f;

        controller.Move(move * speed * Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //rotation player with camera angle
        targetAngle = cam.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        
        //animation
        if (groundedPlayer)
        {
            if (!RunBtn)
            {
                animPlayer.SetFloat("WalkX", moveHorizontal);
                animPlayer.SetFloat("WalkZ", moveVertical);
            }
            else
            {
                animPlayer.SetFloat("WalkX", moveHorizontal * 3f);
                animPlayer.SetFloat("WalkZ", moveVertical * 3f);
            }
        }

        //effect Walk Player
        if(groundedPlayer && moveBtn)
        {
            effectWalkPlayer.SetActive(true);
        }
    }

    IEnumerator effectWalkoff()
    {
        yield return new WaitForSeconds(1f);
        effectWalkPlayer.SetActive(false);
    }

    //for button on Android
    public void onMove()
    {
        
        moveBtn = true;
    }

    public void offMove()
    {
        moveBtn = false;
        StartCoroutine(effectWalkoff());
    }

    public void onJump()
    {
        RunBtn = true;
        weaponsPlayer.RunFast();
    }

    public void offJump()
    {
        RunBtn = false;
    }
}
