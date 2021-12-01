using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats {

    // Singleton
    private static PlayerStats _instance;
    private static PlayerStats Instance {
        get
        {
            if (_instance == null) _instance = new PlayerStats(); // this can happen exactly once in the lifetime of your app
            return _instance;
        }
    }

    //[SerializeField] private int startEnergyPerMove = 100;
    [SerializeField] private int startMaxEnergy = 100;
    [SerializeField] private float startTimerTime = 2f / 60f;

    private int maxEnergy;
    private float timerTime;

    public PlayerStats()
    {
        maxEnergy = startMaxEnergy;
        timerTime = startTimerTime;
    }

    public void Reset()
    {
        maxEnergy = startMaxEnergy;
        timerTime = startTimerTime;
    }
   
    // Energy
    public int GetMaxEnergy()
    {
        return maxEnergy;
    }
    public void SetMaxEnergy(int energy)
    {
        maxEnergy = energy;
    }

    // Timer
    public float GetTimerTime()
    {
        return timerTime;
    }
    public void SetTimerTime(float time)
    {
        timerTime = time;
    }
}
