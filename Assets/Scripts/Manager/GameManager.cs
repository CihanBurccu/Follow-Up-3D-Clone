using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public int levelCount;

    public GameObject UIPanel, EventSystem;


    [Header("Level")]
    public int currentLevel;

   
    [Header("Menus")]
    public GameObject loaderPanel;

    public GameObject successMenu;
    public Button successNextLevelButton;

    public GameObject failMenu;
    public Button failPlayAgainButton;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(UIPanel);
            loaderPanel.SetActive(false);
            SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        }
    }

    private void Start()
    {
        LoadCurrentLevel();
        SetButtonEvents();
    }

    private void SetButtonEvents()
    {

        failPlayAgainButton.onClick.AddListener(() =>
        {
            PlayAgain();
            SetFailMenuState(false);

        });

        successNextLevelButton.onClick.AddListener(() =>
        {
            NextLevel();
            SetSuccessMenuState(false);
        });

    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        SetFailMenuState(false);
        SetSuccessMenuState(false);
    }

    private void PlayAgain()
    {
        LoadLevel(currentLevel);
    }

    public void SetSuccessMenuState(bool state)
    {
        successMenu.SetActive(state);
    }

    public void SetFailMenuState(bool state)
    {
        failMenu.SetActive(state);
    }

    private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }


    public void NextLevel()
    {        
        currentLevel++;
        if (currentLevel > levelCount)
        {
            currentLevel = 1;
        }
        PlayerPrefs.SetInt("Level", currentLevel);
        LoadLevel(currentLevel);
    }

    public void LoadCurrentLevel()
    {
        currentLevel = PlayerPrefs.GetInt("Level", 1);
        if (currentLevel == 1)
        {
            PlayerPrefs.SetInt("Level", 1);
        }
        LoadLevel(currentLevel);        
    }
}
