using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SavePlayerData(int selectedCharacterId_, int coins_, int bestScore_)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(selectedCharacterId_, coins_, bestScore_);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SavePlayerData(PlayerData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData result = new PlayerData(data.selectedCharacterId, data.coins, data.bestScore);

        formatter.Serialize(stream, result);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (!File.Exists(path))
        {
            Debug.LogError("Save file not found");
            return new PlayerData();
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        PlayerData data = formatter.Deserialize(stream) as PlayerData;

        stream.Close();

        return data;
    }
}
