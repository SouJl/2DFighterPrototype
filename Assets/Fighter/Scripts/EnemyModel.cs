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
        private int _currentPlayerCrime;

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
                case DataType.Crime:
                    _currentPlayerCrime = playerData.Value;
                    break;
            }

            Debug.Log($"Notified {_name} change to {playerData.DataType:F}");
        }

        public int CalcPower()
        {
            int healthCoef = GetHelthCoeficient();

            float moneyRatio = _currentPlayerMoney / _calucalationData.MoneyCoef;
            
            float powerRatio = _currentPlayerPower * _calucalationData.PowerCoef;

            float crimeRatio = GetCrimeRatio();

            return (int)(healthCoef + moneyRatio + powerRatio + crimeRatio);
        }

        private int GetHelthCoeficient() =>
            _currentPlayerHealth > _calucalationData.MaxPlayerHealth ? (_currentPlayerHealth * 2) : 3;

        private float GetCrimeRatio()
            => Mathf.Clamp(Mathf.Log(_currentPlayerCrime, 2), 0, _calucalationData.CrimeCoef);
    }
}
