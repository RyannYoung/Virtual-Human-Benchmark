using UnityEngine;

public class StartArrow : MonoBehaviour
{
    public GameStateManager gameStateManager;

    private void OnEnable()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameStateManager.GetSelectedGame() != GameStateManager.Games.None &&
            gameStateManager.GetCurrentGame() == GameStateManager.Games.None)
        {
            gameObject.SetActive(true);
        }
    }
}
