using CamFollow;
using GamePlay;
using GamePlay.Player;
using Infrastructure.Level;
using Infrastructure.Input;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class EntryPoint: MonoBehaviour
    {
        [Header("Level Settings")]
        [SerializeField] private LevelLoader _levelLoader;
        [SerializeField] private float _timeWin;
        [SerializeField] private float _timeLose;
        [Header("Player Settings")]
        [SerializeField] private float _playerSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _playerStartPosition;
        [SerializeField] private Player _characterPrefab;
        [SerializeField] private StackConfig _playerMetalStackConfig;
        [SerializeField] private StackConfig _playerSwordStackConfig;
        [FormerlySerializedAs("_canvasPrefab")]
        [Header("UI")]
        [SerializeField] private UIControl _uiPrefab;

        private LevelManager _levelManager;
        private IInputService _inputService;
        private UIControl _ui;
        private Player _character;
        private CamFollower _camera; 
        private void Awake()
        {
            _levelManager = new LevelManager(_timeLose, _timeWin);
            CreateAndInitUI();
            _inputService = InputService();
            CreateAndInitPlayer();
            CreateAndInitCamera();
        }

        private void CreateAndInitUI()
        {
            _ui = Instantiate(_uiPrefab);
            _ui.Init(_levelManager, _levelManager, _levelLoader);
        }

        private IInputService InputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                return new MobileInputService();
            }
        }

        private void CreateAndInitPlayer()
        {
            _character = Instantiate(_characterPrefab, _playerStartPosition.position, Quaternion.identity);
            _character.Init(_inputService, _playerSpeed, _rotationSpeed, _playerMetalStackConfig, _playerSwordStackConfig);
        }

        private void CreateAndInitCamera()
        {
            _camera = Camera.main.GetComponent<CamFollower>();
            _camera.Init(_levelManager, _character.transform);
        }
    }
}