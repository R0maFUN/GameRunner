using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] public Text scoreText;
    [SerializeField] public Text coinsText;

    // Update is called once per frame
    void Update()
    {
        scoreText.text = GameManager.instance.currentScore.ToString();
        coinsText.text = GameManager.instance.collectedCoins.ToString();
    }
}
