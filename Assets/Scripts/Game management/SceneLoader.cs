using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;

public class SceneLoader : MonoBehaviour
{
    [Header("References to other scripts")]
    [SerializeField] References references;
    [SerializeField] TextMeshProUGUI currencyValue;
    [SerializeField] DataSavingSystem DSS;
    [SerializeField] PlayerMovement PM;
    public PlayerShooting PlayerShooting;

    [Header("UI screens")]
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject youDiedScreen;
    [SerializeField] GameObject pauseMenuScreen;

    [Header("buttons")]
    [SerializeField] Button levelCompleteMenuButton;
    [SerializeField] Button pauseUIMenuButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button levelCompleteNextLevelButton;
    [SerializeField] Button youDiedMenuButton;
    [SerializeField] Button youDiedRestartButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button hangarButton;
    [SerializeField] Button startGameButton;
    
    [Header("Game states")]
    public static bool GameIsPaused;
    private bool playerControlBeforePause;
    
    [Header("Script flow")]
    private static bool coroutineAllowed;
    private bool loadingSceneAllowed;
    private static SceneLoader instance;

    private void Awake()
    {
        SetUpSingleton();

        loadingSceneAllowed = true;

        DSS = FindObjectOfType<DataSavingSystem>();
        references = FindObjectOfType<References>();
    }
    
    private void OnEnable()
    {
        InitializeMajorButtons();
    }

    private void Start()
    {
        coroutineAllowed = true;
    }

    private void OnGUI()
    {
        if(Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            if (GetCurrentSceneByName().Equals("Shop"))
                LoadFirstSceneWithSaving();
            else if(GetCurrentSceneByName() != "Starting scene")
                Pause();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        if (this != instance)
            return;

        if (level >= 0)
        {
            references = FindObjectOfType<References>();

            DSS.LoadJsonData();

            InitializeMajorButtons();
        }

        if (level > 0)
        {
            if (SceneManager.GetActiveScene().name != "Shop")
            {
                InitalizeReferences();
                InitializeIngameButtonsAndUI();
            }
        }
    }

    #region Ingame scene/levels loading

    public void LoadNextSceneWithSaving()
    {
        if (winScreen != null)
            winScreen.SetActive(false);

        StartCoroutine(LoadNextSceneCoroutine());
    }

    private IEnumerator LoadNextSceneCoroutine()
    {
        DSS.SaveJsonData();

        ScreenFade.FadeScreen();
        GameMusic.FadeMusicOut();
        yield return new WaitForSeconds(1.2f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public IEnumerator LoadNextSceneWithTimeMachineAnimation()
    {   
        // This method was created for scripted dialogue scenes without any enemies
        StartCoroutine(PM.FlyOffScreen(fixedDeltaYChange: 0.0005f));

        yield return StartCoroutine(TimeTravelMachineVFX.ActivateTimeMachine());

        yield return new WaitForSeconds(1.8f);

        ScreenFade.FadeScreen();
        GameMusic.FadeMusicOut();
        SetCurrentProgressLevel();
        DSS.SaveJsonData();
        yield return new WaitForSeconds(1.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadFirstSceneWithSaving()
    {
        if (GetCurrentSceneByName() != "Shop")
            AudioManager.PlayAudioByName("Menu sound");

        DSS.SaveJsonData();
        SceneManager.LoadScene(0);
    }

    public void ExitToMainManuWithSaving()
    {
        AudioManager.PlayAudioByName("Menu sound");
        DSS.SaveJsonData();
        ScreenFade.FadeScreen();
        GameMusic.FadeMusicOut();

        if (winScreen != null)
            winScreen.SetActive(false);

        Invoke("LoadFirstSceneWithSaving", 1.3f);
    }

    public void ExitToMainMenuWithoutSaving()
    {
        pauseMenuScreen.SetActive(false);
        youDiedScreen.SetActive(false);
        Time.timeScale = 1f;
        GameMusic.FadeMusicOut();
        ScreenFade.FadeScreen();
        Invoke("LoadFirstSceneWithoutSaving", 1.3f);
    }

    public void LoadFirstSceneWithoutSaving()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(GetCurrentSceneIndex());
    }

    public IEnumerator EndGameDramatically()
    {
        ScreenFade.FadeScreen(5f);
        GameMusic.FadeMusicOut(5f);
        yield return new WaitForSeconds(5f);

        DSS.SaveJsonData();
        SceneManager.LoadScene(0);
    }

    #endregion


    #region Loading scenes/levels from starting menu and shop

    public void LoadCurrentProgressLevel()
    {
        StartCoroutine(LoadProgressLevel()); // Coroutine is wrapped in a method because it's attached to a button
    }

    private IEnumerator LoadProgressLevel()
    {
        if (coroutineAllowed)
        {
            coroutineAllowed = false;

            AudioManager.PlayAudioByName("Menu sound");
            IdleMusic.FadeMusic();
            ScreenFade.FadeScreen();
            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(DataManager.GetCurrentLevel());

            coroutineAllowed = true;
        }
    }

    public void EnterShop()
    {
        if (loadingSceneAllowed)
        {
            loadingSceneAllowed = false;
            DSS.SaveJsonData();
            AudioManager.PlayAudioByName("Menu sound");

            if (GetCurrentSceneByName().Equals("Starting scene"))
            {
                LoadShopScene();
            }
            else
            {
                if (winScreen != null)
                    winScreen.SetActive(false);

                GameMusic.FadeMusicOut();
                ScreenFade.FadeScreen();
                Invoke("LoadShopScene", 1.3f);
            }

            loadingSceneAllowed = true;
        }
    }

    private void LoadShopScene()
    {
        // This method is used for Invoking by string
        SceneManager.LoadScene("Shop");
    }

    #endregion


    #region Game states control

    public IEnumerator InitiateWinning()
    {
        PlayerMovement.playerHasControl = false;
        yield return StartCoroutine(PM.FlyOffScreen());

        winScreen.SetActive(true);
        SetCurrentProgressLevel();
    }

    public static void ActivateDeathScreen()
    {
        PlayerMovement.playerHasControl = false;

        instance.youDiedScreen.SetActive(true);
    }

    public void SetCurrentProgressLevel()
    {
        if (GetCurrentSceneByName().Equals("Level 1-8"))
            return;

        else
            DataManager.SetCurrentLevel(DataManager.GetCurrentLevel() + 1);
    }

    private void Pause()
    {
        pauseMenuScreen.SetActive(true);

        playerControlBeforePause = PlayerMovement.playerHasControl;

        if (playerControlBeforePause == true)
            PlayerMovement.playerHasControl = false;

        GameMusic.PauseMusic();

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuScreen.SetActive(false);

        if (playerControlBeforePause == true)
            PlayerMovement.playerHasControl = true;

        GameMusic.ResumeMusic();

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion


    #region Scene info

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public static string GetCurrentSceneByNameStatic()
    {
        return SceneManager.GetActiveScene().name;
    }

    public string GetCurrentSceneByName()
    {
        return SceneManager.GetActiveScene().name;
    }

    #endregion


    #region UI Initialization

    private void InitializeIngameButtonsAndUI()
    {
        if (SceneManager.GetActiveScene().name != "Shop")
            winScreen = references.GetWinScreen();
        pauseMenuScreen = references.GetPauseMenuUI();
        youDiedScreen = references.GetYouDiedScreen();

        levelCompleteMenuButton = references.GetLevelCompleteMenuButton();
        levelCompleteMenuButton.onClick.AddListener(ExitToMainManuWithSaving);

        levelCompleteNextLevelButton = references.GetLevelCompleteNextLevelButton();
        levelCompleteNextLevelButton.onClick.AddListener(LoadNextSceneWithSaving);

        pauseUIMenuButton = references.GetPauseUIMenuButton();
        pauseUIMenuButton.onClick.AddListener(ExitToMainMenuWithoutSaving);

        resumeButton = references.GetResumeButton();
        resumeButton.onClick.AddListener(Resume);

        youDiedMenuButton = references.GetYouDiedMenuButton();
        youDiedMenuButton.onClick.AddListener(ExitToMainMenuWithoutSaving);

        youDiedRestartButton = references.GetYouDiedRestartButton();
        youDiedRestartButton.onClick.AddListener(PlayAgain);
    }

    private void InitializeMajorButtons()
    {
        if(GetCurrentSceneByName() != "Shop")
        {
            hangarButton = references.GetHangarButton();
            hangarButton.onClick.AddListener(EnterShop);

            quitButton = references.GetQuitButton();
            quitButton.onClick.AddListener(QuitGame);
        }

        if(GetCurrentSceneByName() == "Starting scene")
        {
            startGameButton = references.GetStartGameButton();
            startGameButton.onClick.AddListener(LoadCurrentProgressLevel);
        }
    }

    #endregion

    private void InitalizeReferences()
    {
        PM = references.GetPlayerMovement();

        if (PM != null)
            PlayerShooting = PM.GetComponent<PlayerShooting>();
    }

    private void SetUpSingleton()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
