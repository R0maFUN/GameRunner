using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectorUI : MonoBehaviour
{
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject selectedButton;
    [SerializeField] private GameObject unlockButton;

    [SerializeField] private TMPro.TextMeshProUGUI characterNameText;

    private void Start()
    {
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        if (Player.instance.SelectedCharacterId == CharacterSelector.instance.currentCharacterId)
        {
            selectButton.SetActive(false);
            selectedButton.SetActive(true);
            unlockButton.SetActive(false);
        }
        else if (Player.instance.SelectedCharacterId != CharacterSelector.instance.currentCharacterId)
        {
            selectButton.SetActive(true);
            selectedButton.SetActive(false);
            unlockButton.SetActive(false);
        }

        characterNameText.text = CharacterSelector.instance.currentCharacter.GetComponent<CharacterData>().name;
    }
}
