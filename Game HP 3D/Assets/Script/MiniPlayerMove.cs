using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController character;
    [SerializeField] float speed;
    private bool LookRight;

    void Start()
    {
        character = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(1, 0, 0);
        if (LookRight)
        {
            character.Move(move * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            character.Move(move * -speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            LookRight = !LookRight;
        }
    }
}
