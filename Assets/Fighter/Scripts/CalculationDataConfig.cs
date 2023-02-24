using UnityEngine;

namespace FighterGame
{
    internal interface ICalucalationData
    {
        float MaxPlayerHealth { get; }

        float HealthCoef { get; }
        float MoneyCoef { get; }
        float PowerCoef { get; }

        float CrimeCoef { get; }
    }


    [CreateAssetMenu(fileName = nameof(CalculationDataConfig),
        menuName = "Configs/" + nameof(CalculationDataConfig))]
    internal class CalculationDataConfig : ScriptableObject, ICalucalationData
    {
        [field : SerializeField] public float MaxPlayerHealth { get; private set; } = 20f;

        [field: SerializeField] public float HealthCoef { get; private set; } = 1.2f;

        [field: SerializeField] public float MoneyCoef { get; private set; } = 5f;

        [field: SerializeField] public float PowerCoef { get; private set; } = 0.5f;

        [field: SerializeField] public float CrimeCoef { get; private set; } = 2f;

       
    }
}
