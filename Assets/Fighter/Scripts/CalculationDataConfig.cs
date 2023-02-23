using UnityEngine;

namespace FighterGame
{
    internal interface ICalucalationData
    {
        float MaxPlayerHealth { get; }
        float MoneyCoef { get; }
        float PowerCoef { get; }
    }


    [CreateAssetMenu(fileName = nameof(CalculationDataConfig),
        menuName = "Configs/" + nameof(CalculationDataConfig))]
    internal class CalculationDataConfig : ScriptableObject, ICalucalationData
    {
        [field : SerializeField] public float MaxPlayerHealth { get; private set; } = 20f;

        [field: SerializeField] public float MoneyCoef { get; private set; } = 5f;

        [field: SerializeField] public float PowerCoef { get; private set; } = 1.5f;
    }
}
