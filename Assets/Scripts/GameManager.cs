using DG.Tweening;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    [SerializeField] private Scene winingScene;

    [SerializeField] private int bestOf = 5;

    // WASD player
    [SerializeField] private int Player1 = 0;
    // Mouse player
    [SerializeField] private int Player2 = 0;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = gameObject;
        DontDestroyOnLoad(gameObject);
    }

    public void NextRound(bool player1Win)
    {
        Player1 += player1Win ? 1 : 0;
        Player1 += player1Win ? 0 : 1;

        if (Player1 >= bestOf || Player2 >= bestOf)
            SceneManager.LoadScene(winingScene.name);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        Player1 = 0;
        Player2 = 0;
    }
}
