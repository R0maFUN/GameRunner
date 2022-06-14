using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotionPower : MonoBehaviour, IPower
{

    TimeManager timeManager;
    Movement playerMovement;

    bool isSlowmotioned = false;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = FindObjectOfType<TimeManager>();
        playerMovement = gameObject.GetComponent<Movement>();
    }

    public void StartPower()
    {
        if (isSlowmotioned)
            return;
        timeManager.doSlowMotion();
        isSlowmotioned = true;
        playerMovement.horizontalForce *= 4;
    }

    public void StopPower()
    {
        if (!isSlowmotioned)
            return;
        timeManager.doUnSlowMotion();
        isSlowmotioned = false;
        playerMovement.horizontalForce /= 4;
    }

    public void TriggerPower()
    {
        if (isSlowmotioned)
            StopPower();
        else
            StartPower();
    }
}
