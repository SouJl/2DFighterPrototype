using UnityEngine;

namespace FighterGame
{
    internal interface IEnemy 
    {
        void UpdateSelfData(PlayerData playerData);
    }

    internal class EnemyModel : IEnemy
    {
        private readonly string _name;
        private readonly ICalucalationData _calucalationData;


        private int _currentPlayerHealth;
        private int _currentPlayerMoney;
        private int _currentPlayerPower;

        public EnemyModel(string name, ICalucalationData calucalationData) 
        {
            _name = name;
            _calucalationData = calucalationData;
        }

        public void UpdateSelfData(PlayerData playerData)
        {
            switch (playerData.DataType)
            {
                case DataType.Health:
                    _currentPlayerHealth = playerData.Value;
                    break;

                case DataType.Money:
                    _currentPlayerMoney = playerData.Value;
                    break;
                case DataType.Power:
                    _currentPlayerPower = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int healthCoef = CalcKHealth();
            float moneyRatio = _currentPlayerMoney / _calucalationData.MoneyCoef;
            float powerRatio = _currentPlayerPower / _calucalationData.PowerCoef;

            return (int)(moneyRatio + healthCoef + powerRatio);
        }

        private int CalcKHealth() =>
            _currentPlayerHealth > _calucalationData.MaxPlayerHealth ? 100 : 5;
    }
}
