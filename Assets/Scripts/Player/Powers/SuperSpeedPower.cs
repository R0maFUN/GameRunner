using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperSpeedPower : MonoBehaviour, IPower
{
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float increasingSpeed = 25f;
    [SerializeField] private float decreasingSpeed = 50f;
    [SerializeField] private GameObject bigCollider;
    [SerializeField] private BoxCollider boxCollider;

    private float prevSpeed;
    bool isTurningOn = false;
    bool isTurningOff = false;
    bool isUsing = false;


    // Start is called before the first frame update
    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
        bigCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTurningOn)
        {
            if (RoadGenerator.instance.currentSpeed > maxSpeed)
            {
                isTurningOn = false;
                return;
            }

            RoadGenerator.instance.currentSpeed += increasingSpeed * Time.deltaTime;
            PostProcessVolumeManager.instance.increaseChromaticAberration(0.2f * Time.deltaTime);
        }

        if (isTurningOff)
        {
            if (RoadGenerator.instance.currentSpeed - decreasingSpeed * Time.deltaTime <= prevSpeed)
            {
                isTurningOff = false;
                isUsing = false;
                return;
            }

            RoadGenerator.instance.currentSpeed -= decreasingSpeed * Time.deltaTime;
            PostProcessVolumeManager.instance.increaseChromaticAberration(-0.2f * Time.deltaTime);
        }
    }

    public void StartPower()
    {
        if (isUsing)
            return;
        prevSpeed = RoadGenerator.instance.currentSpeed;
        isUsing = true;
        isTurningOn = true;
        isTurningOff = false;
        //boxCollider.enabled = false;
        bigCollider.SetActive(true);
    }

    public void StopPower()
    {
        if (!isUsing)
            return;
        isUsing = false;
        isTurningOff = true;
        isTurningOn = false;
        //boxCollider.enabled = true;
        bigCollider.SetActive(false);
    }

    public void TriggerPower()
    {
        if (isUsing)
            StopPower();
        else
            StartPower();
    }
}
