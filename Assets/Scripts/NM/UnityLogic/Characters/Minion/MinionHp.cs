using System;
using NM.Data;
using NM.Services.PersistentProgress;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionHp : MonoBehaviour, ISavedProgressWriter, IHealthCharacter
    {
        public int HP { get; private set; }
        public int MaxHp { get; private set; }

        public event Action<int> OnHpChanged;

        private string _id;

        public void Construct(string id, int maxHp)
        {
            _id = id;
            MaxHp = maxHp;
        }
        public void TakeDamage(int damage = 1)
        {
            if (HP <= 0) return;
            
            HP -= damage;
            OnHpChanged?.Invoke(HP);
        }
        public void SaveProgress(SaveSlotData slot)
        {
            foreach (var minionData in slot.MinionsData)
            {
                if (minionData.Id == _id)
                {
                    minionData.Hp = HP;
                    return;
                }
            }

            var newHpData = new HealthCharacterData();
            newHpData.Id = _id;
            newHpData.Hp = HP;
            slot.MinionsData.Add(newHpData);
        }
        public void LoadProgress(SaveSlotData slot)
        {
            foreach (var minion in slot.MinionsData)
            {
                if (minion.Id == _id)
                {
                    SetHp(minion.Hp);
                    return;
                }
            }
            SetHp(MaxHp);
        }
        private void SetHp(int newHpValue)
        {
            HP = newHpValue;
            OnHpChanged?.Invoke(HP);
        }
    }
}