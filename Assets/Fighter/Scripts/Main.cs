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

        [Header("Gameplay Buttons")]
        [SerializeField] private Button _fightButton;

        private PlayerData _money;
        private PlayerData _heath;
        private PlayerData _power;

        private EnemyModel _enemy;

        private void Start()
        {
            _enemy = new EnemyModel("Enemy Punck", _calculationData);

            _money = CreatePlayerData(DataType.Money);
            _heath = CreatePlayerData(DataType.Health);
            _power = CreatePlayerData(DataType.Power);

            Subscribe();
        }

        private void OnDestroy()
        {
            DisposePlayerData(ref _money);
            DisposePlayerData(ref _heath);
            DisposePlayerData(ref _power);

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

            _fightButton.onClick.AddListener(Fight);
        }

        private void Unsubscribe()
        {
            _increaseHealthButton.onClick.RemoveListener(IncreaseHealth);
            _decreaseHealthButton.onClick.RemoveListener(DecreaseHealth);

            _increaseMoneyButton.onClick.RemoveListener(IncreaseMoney);
            _decreaseMoneyButton.onClick.RemoveListener(DecreaseMoney);

            _increasePowerButton.onClick.RemoveListener(IncreasePower);
            _decreasePowerButton.onClick.RemoveListener(DecreasePower);

            _fightButton.onClick.RemoveAllListeners();
        }

        private void IncreaseMoney() => IncreaseValue(_money);
        private void DecreaseMoney() => DecreaseValue(_money);

        private void IncreaseHealth() => IncreaseValue(_heath);
        private void DecreaseHealth() => DecreaseValue(_heath);

        private void IncreasePower() => IncreaseValue(_power);
        private void DecreasePower() => DecreaseValue(_power);

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
                _ => throw new ArgumentException($"Wrong {nameof(DataType)}")
            };

        private void Fight()
        {
            int enemyPower = _enemy.CalcPower();
            bool isVictory = _power.Value >= enemyPower;

            string color = isVictory ? "#07FF00" : "#FF0000";
            string message = isVictory ? "Win" : "Lose";

            Debug.Log($"<color={color}>{message}!!!</color>");
        }

    }
}

