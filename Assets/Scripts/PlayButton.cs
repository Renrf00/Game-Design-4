using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public void Play()
    {
        GameManager.instance.RestartGame();
    }
}
