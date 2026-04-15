using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private TMP_Text ScoreText;

    [SerializeField] private string MenuScene;
    [SerializeField] private string playingScene;

    [SerializeField] private int bestOf = 5;

    // WASD player
    [SerializeField] private int Player1 = 0;
    // Mouse player
    [SerializeField] private int Player2 = 0;


    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void NextRound(bool player1Win)
    {
        Player1 += player1Win ? 1 : 0;
        Player2 += player1Win ? 0 : 1;

        UpdateScore();

        if (Player1 >= bestOf || Player2 >= bestOf)
        {
            SceneManager.LoadScene(MenuScene);
        }
        else
            SceneManager.LoadScene(playingScene);
    }

    private void UpdateScore()
    {
        ScoreText.text = "Keyboard: " + Player1 + " | Mouse: " + Player2;
    } 

    public void RestartGame()
    {
        Player1 = 0;
        Player2 = 0;
        SceneManager.LoadScene(playingScene);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == MenuScene)
        {
            TMP_Text text = GameObject.Find("WinText").GetComponent<TMP_Text>();
            if (Player1 >= bestOf)
                text.text = "Keyboard wins!!";
            else if (Player2 >= bestOf)
                text.text = "Mouse wins!!";
            else
                text.text = "";
        }
    }
}
