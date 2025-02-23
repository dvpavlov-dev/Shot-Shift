using System.Collections.Generic;
using Shot_Shift.Infrastructure.Scripts;
using Shot_Shift.Infrastructure.Scripts.Services;
using Shot_Shift.Infrastructure.Scripts.States;
using UnityEngine;
using Zenject;

namespace Shot_Shift.UI.Scripts.StartScene
{
    public class LevelsWindowController : WindowView
    {
        [SerializeField] private GameObject _levelsContainer;
        [SerializeField] private GameObject _levelsCellPrefab;
        
        private PlayerProgressService _playerProgressService;
        private Infrastructure.Scripts.Configs _configs;
        private GameStateMachine _gameStateMachine;
        private List<GameObject> _levelCells = new();

        [Inject]
        private void Constructor(GameStateMachine gameStateMachine, PlayerProgressService playerProgressService, Infrastructure.Scripts.Configs configs)
        {
            _gameStateMachine = gameStateMachine;
            _configs = configs;
            _playerProgressService = playerProgressService;
        }
        
        private void OnEnable()
        {
            ClearListCells();

            for (int i = 0; i < _configs.LevelsConfig.levels.Count; i++)
            {
                GameObject level = Instantiate(_levelsCellPrefab, _levelsContainer.transform);
                _levelCells.Add(level);
                
                LevelCell levelView = level.GetComponent<LevelCell>();
                TypeCell typeCell;
                
                if (i <= _playerProgressService.LastCompletedLevel)
                {
                    typeCell = i < _playerProgressService.LastCompletedLevel ? TypeCell.PASS : TypeCell.CURRENT;
                }
                else
                {
                    typeCell = TypeCell.NOT_PASS;
                }

                levelView.SetupCell(i, typeCell);
                levelView.OnSelectedLevel += OnSelectedLevel;
            }
        }
        
        private void ClearListCells()
        {
            foreach (GameObject cell in _levelCells)
            {
                Destroy(cell);
            }
            
            _levelCells.Clear();
        }

        private void OnSelectedLevel(int selectedLevel)
        {
            _playerProgressService.ChangeLevelData(selectedLevel);
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}
