using Scripts.Architecture.Services;
using Scripts.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    [RequireComponent(typeof(Slider))]

    public class ZombieHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private IZombieHealthService _zombieHealthService;

        private void OnDisable()
        {
            _zombieHealthService.HealthChanged -= OnHealthChanged;
        }

        private void Start()
        {
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();
            _slider.value = UIConstants.MaximumSliderValue;

            _zombieHealthService.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int value)
        {
            _slider.value = (float)value / ZombieConstants.ZombieMaximumHealth;
        }
    }
}