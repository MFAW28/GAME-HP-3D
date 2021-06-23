using UnityEngine;
using UnityEngine.UI;

public class doorAnim : MonoBehaviour
{
    public Animator animDoor;


    public void OpenDoor()
    {
        animDoor.SetBool("openDoor", true);
    }
}
