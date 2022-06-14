using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.currentScore++;
            GameManager.instance.makeItHarder();

            if (GameManager.instance.currentScore > 4)
                RoadGenerator.instance.RespawnFirstRoad();

            //score.UpdateScore(currentScore);
        }
    }
}
