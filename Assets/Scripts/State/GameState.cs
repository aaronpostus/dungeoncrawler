using UnityEngine;

public abstract class GameState
{
    protected GameStateManager manager;

    public GameState(GameStateManager gameStateManager)
    {
        manager = gameStateManager;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}

// PlayState
public class PlayState : GameState
{
    public PlayState(GameStateManager manager) : base(manager) { }

    public override void Enter() { Debug.Log("Game Start"); }
    public override void Update() { Debug.Log("Game Playing"); }
    public override void Exit() { Debug.Log("Exiting Play State"); }
}

// GameOverState
public class GameOverState : GameState
{
    public GameOverState(GameStateManager manager) : base(manager) { }

    public override void Enter() { Debug.Log("Game Over"); }
    public override void Exit() { Debug.Log("Exiting GameOver State"); }
}

// PauseState
public class PauseState : GameState
{
    public PauseState(GameStateManager manager) : base(manager) { }

    public override void Enter() { Debug.Log("Game Paused"); }
    public override void Exit() { Debug.Log("Resuming Game"); }
}

// MainMenuState
public class MainMenuState : GameState
{
    public MainMenuState(GameStateManager manager) : base(manager) { }

    public override void Enter() { Debug.Log("Main Menu"); }
    public override void Update() { Debug.Log("In Main Menu"); }
    public override void Exit() { Debug.Log("Exiting Main Menu"); }
}


