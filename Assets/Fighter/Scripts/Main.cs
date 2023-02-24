using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FighterGame
{
    internal class Main : MonoBehaviour
    {
        [SerializeField] CalculationDataConfig _calculationData;

        [Header("Player Stats")]
        [SerializeField] private TMP_Text _playerHealthText;
        [SerializeField] private TMP_Text _playerMoneyText;
        [SerializeField] private TMP_Text _playerPowerText;
        [SerializeField] private TMP_Text _playerCrimeText;

        [Header("Enemy Stats")]
        [SerializeField] private TMP_Text _enemyPowerText;

        [Space(10)]
        [Header("Buttons")]
        [Space(10)]

        [Header("Health Buttons")]
        [SerializeField] private Button _increaseHealthButton;
        [SerializeField] private Button _decreaseHealthButton;

        [Header("Money Buttons")]
        [SerializeField] private Button _increaseMoneyButton;
        [SerializeField] private Button _decreaseMoneyButton;

        [Header("Power Buttons")]
        [SerializeField] private Button _increasePowerButton;
        [SerializeField] private Button _decreasePowerButton;

        [Header("Crime Buttons")]
        [SerializeField] private Button _increaseCrimeButton;
        [SerializeField] private Button _decreaseCrimeButton;

        [Header("Gameplay Buttons")]
        [SerializeField] private Button _fightButton;
        [SerializeField] private Button _escapeButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;
        private PlayerData _crime;

        private EnemyModel _enemy;

        private void Start()
        {
            _enemy = new EnemyModel("Enemy Punck", _calculationData);

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);
            _crime = CreatePlayerData(DataType.Crime);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);
            DisposePlayerData(ref _crime);

            Unsubscribe();
        }

        private PlayerData CreatePlayerData(DataType dataType)
        {
            PlayerData playerData = new PlayerData(dataType);
            playerData.Attach(_enemy);

            return playerData;
        }

        private void DisposePlayerData(ref PlayerData playerData)
        {
            playerData.Detach(_enemy);
            playerData = null;
        }

        private void Subscribe()
        {

            _increaseHealthButton.onClick.AddListener(IncreaseHealth);
            _decreaseHealthButton.onClick.AddListener(DecreaseHealth);

            _increaseMoneyButton.onClick.AddListener(IncreaseMoney);
            _decreaseMoneyButton.onClick.AddListener(DecreaseMoney);

            _increasePowerButton.onClick.AddListener(IncreasePower);
            _decreasePowerButton.onClick.AddListener(DecreasePower);

            _increaseCrimeButton.onClick.AddListener(IncreaseCrime);
            _decreaseCrimeButton.onClick.AddListener(DecreaseCrime);

            _fightButton.onClick.AddListener(Fight);
            _escapeButton.onClick.AddListener(Escape);
        }

        private void Unsubscribe()
        {
            _increaseHealthButton.onClick.RemoveListener(IncreaseHealth);
            _decreaseHealthButton.onClick.RemoveListener(DecreaseHealth);

            _increaseMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _decreaseMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _increasePowerButton.onClick.RemoveListener(IncreasePower);
            _decreasePowerButton.onClick.RemoveListener(DecreasePower);

            _increaseCrimeButton.onClick.RemoveListener(IncreaseCrime);
            _decreaseCrimeButton.onClick.RemoveListener(DecreaseCrime);

            _fightButton.onClick.RemoveListener(Fight);
            _escapeButton.onClick.RemoveListener(Escape);
        }

        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

        private void IncreaseCrime() => IncreaseValue(_crime);
        private void DecreaseCrime() => DecreaseValue(_crime);

        private void IncreaseValue(PlayerData playerData)
        {
            AddToValue(1, playerData);
            UpdateUI(playerData);
        }

        private void DecreaseValue(PlayerData playerData)
        {
            AddToValue(-1, playerData);
            UpdateUI(playerData);
        }

        private void AddToValue(int addition, PlayerData playerData)
        {
            playerData.Value += addition;
        }

        private void UpdateUI(PlayerData playerData)
        {
            ChangeTextData(playerData);
            UpdateEscapeButtonVisibility();
        }

        private void ChangeTextData(PlayerData playerData)
        {
            int value = playerData.Value;
            DataType dataType = playerData.DataType;
            TMP_Text textComponent = GetTextComponent(dataType);
            textComponent.text = $"Player {dataType:F}: {value}";

            int enemyPower = _enemy.CalcPower();
            _enemyPowerText.text = $"Enemy Power: {enemyPower}";
        }

        private TMP_Text GetTextComponent(DataType dataType) =>
            dataType switch
            {
                DataType.Health => _playerHealthText,
                DataType.Money => _playerMoneyText,
                DataType.Power => _playerPowerText,
                DataType.Crime => _playerCrimeText,
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void UpdateEscapeButtonVisibility()
        {
            const int minEnableValue = 0;
            const int maxEnableValue = 2;
            const int minVisibleValue = 0;
            const int maxVisibleValue = 5;

            float crimeValue = _crime.Value;

            bool isEnable = minEnableValue <= crimeValue && crimeValue <= maxEnableValue;
            bool isVisible = minVisibleValue <= crimeValue && crimeValue <= maxVisibleValue;

            _escapeButton.interactable = isEnable;
            _escapeButton.gameObject.SetActive(isVisible);

        }


        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string message = isVictory ? "Win" : "Lose";
            string color = isVictory ? "#07FF00" : "#FF0000";

            LogInConsoleWithColor(message, color);

        }

        private void Escape()
        {
            string message = "Escaped";
            string color = "#FFCF40";

            LogInConsoleWithColor(message, color);

        }

        private void LogInConsoleWithColor(string logMessage, string logColor) =>
            Debug.Log($"<color={logColor}>{logMessage}!!!</color>");

    }
}

