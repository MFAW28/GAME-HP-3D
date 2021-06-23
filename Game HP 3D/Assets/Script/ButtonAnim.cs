using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAnim : MonoBehaviour
{

    private Button thisButton;
    private Animator animButton;

    void Start()
    {
        thisButton = GetComponent<Button>();
        animButton = GetComponent<Animator>();
        thisButton.onClick.AddListener(TaskOnClick);
    }
    
    void TaskOnClick()
    {
        StartCoroutine(PressButton());
    }

    IEnumerator PressButton()
    {
        animButton.SetBool("PressButton", true);
        yield return new WaitForSeconds(0.7f);
        animButton.SetBool("PressButton", false);
    }
}
