using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //public TimeManager timeManager;
    //public RoadGenerator roadGenerator;
    public bool isPaused = false;
    public int currentScore = 0;
    public int collectedCoins = 0;
    public int selectedCharacterId;

    public float bonusSpawnProbability = 0.9f;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject homeScreenUI;
    [SerializeField] private GameObject gameScreenUI;
    [SerializeField] private GameObject gameOverScreenUI;
    [SerializeField] private GameObject playerData;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager start()");
        ShowHomeScreenUI();
        Instantiate(playerData, new Vector3(0, 0, 0), Quaternion.identity);
        CharacterLoader.instance.CreatePlayerCharacter();
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Unpause();
        FollowPlayer cameraScript = FindObjectOfType<FollowPlayer>();
        cameraScript.ClipCameraToPlayer();
        ShowGameScreenUI();

        RoadGenerator.instance.StartLevel();
    }

    public void makeItHarder()
    {
        float increaseSpeedValue = 0.4f;
        float currentSpeed = RoadGenerator.instance.currentSpeed;
        increaseSpeedValue = increaseSpeedValue * 13 / currentSpeed;
        RoadGenerator.instance.increaseSpeed(increaseSpeedValue);
    }

    public void Pause()
    {
        Debug.Log("Pause");
        if (isPaused)
        {
            Unpause();
            return;
        }    
        isPaused = true;
        Time.timeScale = 0f;
        ShowPauseMenu();
    }

    public void Unpause()
    {
        Debug.Log("Unpause");
        isPaused = false;
        Time.timeScale = 1f;
        ShowGameScreenUI();
    }

    public void GoHome()
    {
        Unpause();
        Debug.Log("Going home");
        SceneManager.LoadScene("MainScene");
    }

    public void GameOver()
    {
        Player.instance.Coins = Player.instance.Coins + collectedCoins;
        isPaused = true;
        Time.timeScale = 0f;
        ShowGameOverScreen();
    }

    public void GoCharacterSelection()
    {
        SceneManager.LoadScene("CharacterSelector");
    }

    private void ShowPauseMenu()
    {
        homeScreenUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        gameOverScreenUI.SetActive(false);
        //gameScreenUI.SetActive(false);
    }

    private void ShowGameScreenUI()
    {
        homeScreenUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameScreenUI.SetActive(true);
        gameOverScreenUI.SetActive(false);
    }

    private void ShowHomeScreenUI()
    {
        homeScreenUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        gameScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(false);
    }

    private void ShowGameOverScreen()
    {
        homeScreenUI.SetActive(false);
        pauseMenuUI.SetActive(false);
        gameScreenUI.SetActive(false);
        gameOverScreenUI.SetActive(true);
    }
}
