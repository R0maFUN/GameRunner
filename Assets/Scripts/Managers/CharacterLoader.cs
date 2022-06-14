using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoader : Singleton<CharacterLoader>
{
    [SerializeField] List<GameObject> characterPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Character loader start()");
    }

    public void CreatePlayerCharacter()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        int selectedCharacterId = data.selectedCharacterId;

        GameObject prefab = characterPrefabs.Find(x => x.GetComponent<CharacterData>().id == selectedCharacterId);

        if (prefab == null)
        {
            Debug.LogError("Can't find character with id = " + selectedCharacterId);
        }

        GameObject player = Instantiate(prefab, new Vector3(0, 0.5f, 0), Quaternion.identity);
    }
}
