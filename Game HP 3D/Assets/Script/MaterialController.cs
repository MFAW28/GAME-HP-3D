using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    [SerializeField] private Renderer renderPlayer;
    [SerializeField] private Material[] AllMaterial;
    public int NumofMaterial;

    void Awake()
    {
        LoadPlayerData();
    }

    void Update()
    {
        renderPlayer.material = AllMaterial[NumofMaterial];
    }

    public void BtnChangeMaterial(int Num)
    {
        FindObjectOfType<AudioManager>().Play("Button");
        NumofMaterial = Num;
    }

    public void PickColour()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SaveSystem.SavePlayer(this);
    }

    public void CancelPickColour()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        LoadPlayerData();
    }

    public void LoadPlayerData()
    {
        PlayerData material = SaveSystem.LoadPlayer(this);
        NumofMaterial = material.NumMaterialPlayer;
    }
}
