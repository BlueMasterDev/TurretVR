using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battery : MonoBehaviour
{
    private float _currentEnergyLevel = 1; //Va de 0 à 1. 0 est vide, 1 la batterie est pleine.

    [SerializeField]
    private Slider m_EnergySlider;

    public void ChangeCharge(float energyChange) //Utiliser une valeure positive pour charger, une valeur négative pour vider
    {
        _currentEnergyLevel = Mathf.Clamp01(_currentEnergyLevel + energyChange);

        m_EnergySlider.value = _currentEnergyLevel;
    }

    public float GetCharge()
    {
        return _currentEnergyLevel;
    }
}
