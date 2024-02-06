using System;

namespace Scripts.Architecture.Services
{
    public class PlayerMoneyService : IPlayerMoneyService
    {
        private readonly int _addReward = 100;
        private readonly ISaveLoadService _saveLoadService;

        public PlayerMoneyService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public event Action MoneyChanged;

        public int Money => _saveLoadService.PlayerProgress.Money;
        public int AddReward => _addReward;

        public bool IsEnoughMoney(int value)
        {
            return _saveLoadService.PlayerProgress.Money >= value;
        }

        public void AddMoney(int value)
        {
            _saveLoadService.PlayerProgress.Money += value;

            MoneyChanged?.Invoke();
            _saveLoadService.SaveProgress();
        }

        public void SpendMoney(int value)
        {
            if (_saveLoadService.PlayerProgress.Money >= value)
            {
                _saveLoadService.PlayerProgress.Money -= value;

                MoneyChanged?.Invoke();
                _saveLoadService.SaveProgress();
            }
        }
    }
}
