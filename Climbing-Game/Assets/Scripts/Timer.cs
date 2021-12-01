using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private bool active = false;
    private float currentTime;
    private float maxTime; // store the original timer's time

    public StatBar timerBar;

    private PlayerStats playerStats;
    
    void Start()
    {
        playerStats = new PlayerStats();

        currentTime = playerStats.GetTimerTime() * 60;
        maxTime = currentTime;

        timerBar.SetMaxValue(maxTime);
    }

    void Update()
    {
        if (active)
        {
            currentTime -= Time.deltaTime;
        }

        timerBar.SetValue(currentTime); // change the bar
    }

    public void ToggleTimer()
    {
        active = !active;
      //  Debug.Log("Timer active: " + active.ToString());
    }
    public void ToggleTimer(bool value)
    {
        active = value;
    }
    public float GetCurrentTime()
    {
        return currentTime;
    }
    public void ResetTimer()
    {
        timerBar.SetValue(maxTime);
        currentTime = maxTime;
        active = false;
    }
}
