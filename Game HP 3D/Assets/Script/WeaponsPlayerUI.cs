using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsPlayerUI : MonoBehaviour
{
    [SerializeField] private Slider sliderWeapons;
    public void SetMaxWeapons(int weaponsHealth)
    {
        sliderWeapons.maxValue = weaponsHealth;
        sliderWeapons.value = weaponsHealth;
    }

    public void SetWeapons(int weaponsHealth)
    {
        sliderWeapons.value = weaponsHealth;
    }
}
