using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private GameStateManager gameStateManager;

    void Start()
    {
        gameStateManager = new GameStateManager();

        // Initialize to whatever state you want the game to start in
        gameStateManager.ChangeState(new MainMenuState(gameStateManager));
    }

    void Update()
    {
        gameStateManager.Update();
    }

    // Example methods to transition between states
    public void StartGame()
    {
        gameStateManager.ChangeState(new PlayState(gameStateManager));
    }

    public void PauseGame()
    {
        gameStateManager.ChangeState(new PauseState(gameStateManager));
    }

    public void GameOver()
    {
        gameStateManager.ChangeState(new GameOverState(gameStateManager));
    }

    public void ReturnToMainMenu()
    {
        gameStateManager.ChangeState(new MainMenuState(gameStateManager));
    }
}
