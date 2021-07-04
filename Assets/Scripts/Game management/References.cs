using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class References : MonoBehaviour
{
    private static References instance;

    [Header("Fields to return to other scripts in scene")]
    [SerializeField] Transform playerPos;
    [SerializeField] GameObject hitVisualEffect;
    [SerializeField] TextMeshProUGUI currencyValue;
    [SerializeField] DataManager PD;
    [SerializeField] DataSavingSystem DSS;
    [SerializeField] PlayerMovement PM;
    [SerializeField] SceneLoader SL;
    [SerializeField] GameObject dialogueManagerOne;
    [SerializeField] GameObject dialogueManagerTwo;
    [SerializeField] AudioManager audioManager;
    public static Transform playerPositionStatic;

    [Header("UI and related buttons")]
    [SerializeField] GameObject winScreen;
    [SerializeField] Button levelCompleteMenuButton;
    [SerializeField] Button levelCompleteNextLevelButton;
    [SerializeField] Button hangarButton;

    [SerializeField] GameObject youDiedScreen;
    [SerializeField] Button youDiedMenuButton;
    [SerializeField] Button youDiedRestartButton;
    [SerializeField] Button quitButton;

    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] Button pauseUIMenuButton;
    [SerializeField] Button resumeButton;

    [SerializeField] Button startGameButton;
    
    private void Awake()
    {
        instance = this;

        PD = FindObjectOfType<DataManager>(); 
        DSS = FindObjectOfType<DataSavingSystem>();
        SL = FindObjectOfType<SceneLoader>();
        audioManager = FindObjectOfType<AudioManager>();

        playerPositionStatic = playerPos;
    }

    public Button GetQuitButton() { return quitButton; }

    public Button GetHangarButton() { return hangarButton; }

    public GameObject GetPauseMenuUI() { return pauseMenuUI; }

    public Transform GetPlayerPos() { return playerPos; }

    public Button GetLevelCompleteMenuButton() { return levelCompleteMenuButton; }

    public GameObject GetHitExplosionEffect() { return hitVisualEffect; }

    public GameObject GetWinScreen() { return winScreen; }

    public TextMeshProUGUI GetCurrencyValue() { return currencyValue; }

    public DataSavingSystem GetDataSavingSystem() { return DSS; }

    public DataManager GetProgressData() { return PD; }

    public PlayerMovement GetPlayerMovement() { return PM; }

    public static PlayerMovement GetPlayerMovementStatic() { return instance.PM; }

    public SceneLoader GetSceneLoader() { return SL; }

    public Button GetResumeButton() { return resumeButton; }

    public Button GetPauseUIMenuButton() { return pauseUIMenuButton; }

    public GameObject GetDialogueManagerOne() { return dialogueManagerOne; }

    public GameObject GetDialogueManagerTwo() { return dialogueManagerTwo; }

    public Button GetLevelCompleteNextLevelButton() { return levelCompleteNextLevelButton; }

    public Button GetYouDiedMenuButton() { return youDiedMenuButton; }

    public Button GetYouDiedRestartButton() { return youDiedRestartButton; }

    public GameObject GetYouDiedScreen() { return youDiedScreen; }

    public AudioManager GetAudioManager() { return audioManager; }

    public Button GetStartGameButton() { return startGameButton; }
}
