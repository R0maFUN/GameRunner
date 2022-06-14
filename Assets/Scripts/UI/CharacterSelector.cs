using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : Singleton<CharacterSelector>
{

    [SerializeField] public List<GameObject> characterPrefabs;

    public GameObject currentCharacter;
    public GameObject previousCharacter;
    public GameObject nextCharacter;

    public int currentCharacterId = 0;
    private int currentIndex = 0;

    private Vector3 prevPosition = new Vector3(-3f, 0.3f, -1.7f);
    private Vector3 currentPosition = new Vector3(0f, 0f, 0f);
    private Vector3 nextPosition = new Vector3(3f, 0.3f, -1.7f);

    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 0;
        characterPrefabs[currentIndex].transform.position = currentPosition;
        for(int i = 1; i < characterPrefabs.Count; ++i)
        {
            characterPrefabs[i].transform.position = nextPosition;
        }
        currentCharacter = characterPrefabs[currentIndex];
        currentCharacterId = currentCharacter.GetComponent<CharacterData>().id;
    }

    public void ShowNext()
    {
        if (currentIndex >= characterPrefabs.Count - 1)
            return;

        characterPrefabs[currentIndex].GetComponent<SelectionMovement>().MoveTo(prevPosition);
        currentIndex++;
        characterPrefabs[currentIndex].GetComponent<SelectionMovement>().MoveTo(currentPosition);
        currentCharacter = characterPrefabs[currentIndex];
        currentCharacterId = currentCharacter.GetComponent<CharacterData>().id;
    }

    public void ShowPrev()
    {
        if (currentIndex <= 0)
            return;

        characterPrefabs[currentIndex].GetComponent<SelectionMovement>().MoveTo(nextPosition);
        currentIndex--;
        characterPrefabs[currentIndex].GetComponent<SelectionMovement>().MoveTo(currentPosition);
        currentCharacter = characterPrefabs[currentIndex];
        currentCharacterId = currentCharacter.GetComponent<CharacterData>().id;
    }

    public void SelectCurrent()
    {
        Player.instance.SelectedCharacterId = characterPrefabs[currentIndex].GetComponent<CharacterData>().id;
    }

    public void GoBack()
    {
        SceneManager.LoadScene("MainScene");
    }
}
