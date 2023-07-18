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
        [SerializeField] private NavMeshSettings _navMeshSettings;
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
        [Header("SpawnerSettings")]
        [SerializeField] private Spawner _spawnerPrefab;
        [SerializeField] private Transform _spawnerPosition;
        [SerializeField] private StackConfig _spawnerStackConfig;
        [SerializeField] private float _spawnerDeliveryTime;
        [SerializeField] private float _spawnerItemHeight;
        [Header("FactorySettings")]
        [SerializeField] private Factory _factoryPrefab;
        [SerializeField] private Transform _factoryPosition;
        [SerializeField] private StackConfig _factoryStackConfig;
        [SerializeField] private float _factoryDeliveryTime;
        [SerializeField] private float _factoryItemHeight;
        [Header("FactorySettings")]
        [SerializeField] private Stock _stockPrefab;
        [SerializeField] private Transform _stockPosition;

        private LevelManager _levelManager;
        private IInputService _inputService;
        private UIControl _ui;
        private Player _character;
        private CamFollower _camera;
        private Spawner _spawner;
        private Factory _factory;
        private Stock _stock;
        private void Awake()
        {
            _levelManager = new LevelManager(_timeLose, _timeWin);
            CreateAndInitUI();
            _inputService = InputService();
            CreateAndInitPlayer();
            CreateAndInitCamera();
            CreateAndInitSpawner();
            CreateAndInitFactory();
            CreateStock();
            _navMeshSettings.UpdateNavMesh();
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

        private void CreateAndInitSpawner()
        {
            _spawner = Instantiate(_spawnerPrefab);
            _spawner.transform.position = _spawnerPosition.position;
            _spawner.Init(_spawnerStackConfig, _spawnerDeliveryTime, _spawnerItemHeight);
        }

        private void CreateAndInitFactory()
        {
            _factory = Instantiate(_factoryPrefab);
            _factory.transform.position = _factoryPosition.position;
            _factory.Init(_factoryStackConfig, _factoryDeliveryTime, _factoryItemHeight);
        }

        private void CreateStock()
        {
            _stock = Instantiate(_stockPrefab);
            _stock.transform.position = _stockPosition.position;
        }
    }
}