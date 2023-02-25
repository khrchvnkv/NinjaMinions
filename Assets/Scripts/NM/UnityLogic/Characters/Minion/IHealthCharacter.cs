using System;

namespace NM.UnityLogic.Characters.Minion
{
    public interface IHealthCharacter
    {
        int HP { get; }
        int MaxHp { get; }
        event Action<int> OnHpChanged;
    }
}