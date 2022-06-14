using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private int selectedCharacterId;
    private int coins;
    private int bestScore;

    public int SelectedCharacterId
    {
        get { return selectedCharacterId; }
        set
        {
            selectedCharacterId = value;
            SaveData();
        }
    }

    public int Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            SaveData();
        }
    }

    public int BestScore
    {
        get { return bestScore; }
        set
        {
            bestScore = value;
            SaveData();
        }
    }

    private void Awake()
    {
        base.Awake();
        PlayerData data = LoadData();
        selectedCharacterId = data.selectedCharacterId;
        coins = data.coins;
        bestScore = data.bestScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        //PlayerData data = LoadData();
        //selectedCharacterId = data.selectedCharacterId;
        //coins = data.coins;
        //bestScore = data.bestScore;

        //Debug.Log("Player start, selectedCharacter = " + selectedCharacterId);
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerData(new PlayerData(this));
    }

    public PlayerData LoadData()
    {
        return SaveSystem.LoadPlayerData();
    }
}
