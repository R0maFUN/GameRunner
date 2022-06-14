using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    [SerializeField] private float slowdownFactor = 0.2f;
    [SerializeField] private float unSlowdownSpeed = 2f;

    private bool unSlowMotion = false;

    private void Update()
    {
        //Debug.Log("Time.timeScale = " + Time.timeScale + " fixedDeltaTime = " + Time.fixedDeltaTime);
        if (unSlowMotion && Time.timeScale != 1f)
        {
            Time.timeScale += (1f / unSlowdownSpeed) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
            if (Time.timeScale >= 0.99f)
                Time.fixedDeltaTime = 0.01f;
        }
    }
    public void doSlowMotion()
    {
        unSlowMotion = false;
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void doUnSlowMotion()
    {
        unSlowMotion = true;
    }
}
