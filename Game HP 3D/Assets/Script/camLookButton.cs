using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class camLookButton : MonoBehaviour
{
    private CinemachineFreeLook cinemachine;
    [SerializeField] private float lookSpeed = 1f;

    [SerializeField] private FixedTouchField touchFieldLook;
    [SerializeField] private FixedTouchField touchFieldAttack;

    private bool lookBtn;
    private bool attackBtn;

    private void Awake()
    {
        cinemachine = GetComponent<CinemachineFreeLook>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta;
        if (lookBtn)
        {
            delta = new Vector2(touchFieldLook.TouchDist.x, touchFieldLook.TouchDist.y);
            cinemachine.m_XAxis.Value += delta.x * 20f * lookSpeed * Time.deltaTime;
            cinemachine.m_YAxis.Value += -delta.y * .1f * lookSpeed * Time.deltaTime;
        }
        else if (attackBtn)
        {
            delta = new Vector2(touchFieldAttack.TouchDist.x, touchFieldAttack.TouchDist.y);
            cinemachine.m_XAxis.Value += delta.x * 20f * lookSpeed * Time.deltaTime;
            cinemachine.m_YAxis.Value += -delta.y * .1f * lookSpeed * Time.deltaTime;
        }
    }

    public void onLookBtn()
    {
        lookBtn = true;
    }

    public void offLookBtn()
    {
        lookBtn = false;
    }

    public void onAttackBtn()
    {
        attackBtn = true;
    }

    public void offAttackBtn()
    {
        attackBtn = false;
    }
}
