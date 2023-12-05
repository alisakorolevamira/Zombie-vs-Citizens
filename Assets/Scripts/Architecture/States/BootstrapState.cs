using Scripts.Architecture.Services;
using UnityEngine;
using Scripts.Architecture.Factory;
using Scripts.Spawner;

namespace Scripts.Architecture.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string Menu = "Menu";
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly GameObject _spawner;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services, GameObject spawner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            _spawner = spawner;

            RegisterService();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }


        public void Exit() { }

        private void RegisterService()
        {
            _services.RegisterSingle<IGameFactory>(new GameFactory());
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService());
            _services.RegisterSingle<IPlayerProgressService>(new PlayerProgressService(_services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IPlayerMoneyService>(new PlayerMoneyService(_services.Single<IPlayerProgressService>()));
            _services.RegisterSingle<IZombieRewardService>(new ZombieRewardService(_services.Single<IPlayerMoneyService>()));
            _services.RegisterSingle<IZombieProgressService>(new ZombieProgressService(_services.Single<ISaveLoadService>()));
            _services.RegisterSingle<IZombieHealthService>(new ZombieHealthService(_services.Single<IZombieProgressService>(),
                _services.Single<IZombieRewardService>()));
            _services.RegisterSingle<ISpawnerService>(new SpawnerService(_spawner.GetComponent<PanelSpawner>(),
                _spawner.GetComponent<SitizenSpawner>(), _spawner.GetComponent<ZombieSpawner>()));
        }

        private void EnterLoadLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(Menu);
        }
    }
}