using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    private bool active = false;
    private float currentEnergy;

    [SerializeField] private float maxEnergy = 100;

    public StatBar energyBar;
    private PlayerStats playerStats;

    void Start()
    {
        playerStats = new PlayerStats();

        currentEnergy = playerStats.GetMaxEnergy();//maxEnergy;
        energyBar.SetMaxValue(currentEnergy);
    }

    void Update()
    {
        if (active)
        {
            currentEnergy -= Time.deltaTime * 4;
        }

        energyBar.SetValue(currentEnergy); // change the bar
    }

    public void ToggleEnergy()
    {
        active = !active;
    //    Debug.Log("Is energy bar active: " + active);
    }
    public void ToggleEnergy(bool value)
    {
        active = value;
    }
    public float GetCurrentEnergyLevel()
    {
        return currentEnergy;
    }
    public void ResetEnergy()
    {
        energyBar.SetValue(maxEnergy);
        currentEnergy = maxEnergy;
        active = false;
    }
}
