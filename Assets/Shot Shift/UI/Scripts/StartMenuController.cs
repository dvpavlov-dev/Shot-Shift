using Shot_Shift.Infrastructure.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _startMenu;
    
    private GameStateMachine _gameStateMachine;

    [Inject]
    private void Constructor(GameStateMachine gameStateMachine)
    {
        _gameStateMachine = gameStateMachine;

    }
    
    public void OnStartSelected()
    {
        // SceneManager.LoadScene("GameLoop");
        _gameStateMachine.Enter<GameLoopState>();
    }
}
