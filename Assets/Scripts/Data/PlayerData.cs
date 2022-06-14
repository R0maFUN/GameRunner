using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int selectedCharacterId;
    public int coins;
    public int bestScore;

    public PlayerData(int selectedCharacterId_, int coins_, int bestScore_)
    {
        selectedCharacterId = selectedCharacterId_;
        coins = coins_;
        bestScore = bestScore_;
    }

    public PlayerData()
    {
        selectedCharacterId = 0;
        coins = 0;
        bestScore = 0;
    }

    public PlayerData(Player player)
    {
        selectedCharacterId = player.SelectedCharacterId;
        coins = player.Coins;
        bestScore = player.BestScore;
    }
}
