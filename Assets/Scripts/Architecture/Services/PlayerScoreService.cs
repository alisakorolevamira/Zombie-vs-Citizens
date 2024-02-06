using System;

namespace Scripts.Architecture.Services
{
    public class PlayerScoreService : IPlayerScoreService
    {
        private const int _loseValue = 1000;
        private readonly ISaveLoadService _saveLoadService;

        public PlayerScoreService(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public event Action ScoreChanged;

        public int Score => _saveLoadService.PlayerProgress.Score;

        public void AddScore(int value)
        {
            _saveLoadService.PlayerProgress.Score += value;

            ScoreChanged?.Invoke();
            _saveLoadService.SaveProgress();
        }

        public void RemoveScore()
        {
            if (_saveLoadService.PlayerProgress.Score >= _loseValue)
                _saveLoadService.PlayerProgress.Score -= _loseValue;

            else
                _saveLoadService.PlayerProgress.Score = 0;

            ScoreChanged?.Invoke();
            _saveLoadService.SaveProgress();
        }
    }
}